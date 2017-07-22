using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.IO;
using System.Diagnostics;
using System.Collections;

/*      主要結構圖
 *          Main        →      MainWindowClose
 *          ↓
 *      change_channel
 *          ↓
 *      IrcSession(StartSession())  →   Channel_viewer
 *          ↓
 *      Enter_IRC_Room  →      IRCRoom_AutoPing
 *          ↓
 *      Msg_proc()
 *          ↓
 *      Who_Talk, Room_Content_Proccess
 *          ↓
 *      SpeechTheText
 * 
 * 
 * 
 */

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private string current_channel;
        private string current_OAuth;
        private IrcClient irc;

        bool ISWIN7 = false;
        
        private static Queue original_msg_qu = new Queue();
        private static Queue tts_o_qu = new Queue();
        Queue OriMsg_object_queue = Queue.Synchronized(original_msg_qu);
        Queue TTS_object_queue = Queue.Synchronized(tts_o_qu);

        private static bool DoNotTalkFunction = false; //控制防搶話是否開啟
        private static bool SE_CANCEL_FUNC = false;
        private static bool AUTO_LOGIN = false;
        private static bool DISTURB_STATE = false;
        private static bool SPEAK_PAUSE = false;
        private static bool SE_PAUSE = false;
        private static bool COMMAND_SPEECH = false;

        private static int Speech_Volume = 100;
        private static int Speech_Rate = 1;
        private static int Speech_Pitch = 0;
        private static float SE_Volume = 1;
        private static int Speech_Word_Limit = 20;
        private static int Min_Btis_Limit = 1;

        private static bool Mic_exist = false;
        private static int DoNotDisturbThreshold = 13;
        private Object thislock = new object();

        List<NickName> NickNameList;
        List<Education> EducationList;
        List<SE> SEList;
        List<ReAct> ReActList;
        List<Except> ExceptList; //排除棒讀的名單

        Thread msg_reader = null;
        Thread Do_Not_Disturb = null;
        Thread IRCRoom = null;
        Thread TTS_T = null;
        Thread AutoPing = null;
        Thread GC_T = null;

        public MainWindow()
        {
            SplashScreen splash = new SplashScreen("splash.jpg");
            splash.Show(true);

            InitializeComponent();

            NickNameList = new List<NickName>();
            EducationList = new List<Education>();
            SEList = new List<SE>();
            ReActList = new List<ReAct>();
            ExceptList = new List<Except>();

            Detect_OS();    //Check OS Environment

            string Settingpath = "Setting";
            string SEpath = "SE";
            if (!Directory.Exists(Settingpath))    //Check the folder "Setting" is exist or not.
                Directory.CreateDirectory(Settingpath);
            if (!Directory.Exists(SEpath))    //Check the folder "SE" is exist or not.
                Directory.CreateDirectory(SEpath);

            ReadPersonalSetting();  //AutoLogin在prisetting.txt裡,因此要先讀出來
            ReadNickNameFile();
            ReadEducationFile();
            ReadReActFile();
            ReadExceptFile();
            GetSEFileList();

            TTS_T = new Thread(SpeechQueue_Puller);
            TTS_T.IsBackground = true;
            TTS_T.Start();

            msg_reader = new Thread(Msg_Reader);
            msg_reader.IsBackground = true;
            msg_reader.Start();

            GC_T = new Thread(GC_Thread);
            GC_T.IsBackground = true;
            GC_T.Start();
            DoNotDisturbInit();

            PutSystemMsg("主程序初始化完成\n\n", Brushes.Green);

            if (AUTO_LOGIN)
            {
                ReadLoginFile();
                try
                {
                    StartSession();
                }
                catch(Exception ex)
                {
                    PutSystemMsg("主程序初始化連線失敗 => StartSession()\n => " + ex.Message + "\n", Brushes.Red);
                }
            }

        }

        public void change_channel_ID(string id)
        {
            current_channel = id;
            current_ID_text.Content = id;
        }
        
        
        
        private void keyin_command(object sender, KeyEventArgs e)       //發話框處理
        {
            if (e.Key == Key.Return)
            {
                if (current_ID_text.Content.ToString() == "尚未登入")
                {
                    PutSystemMsg("您尚未登入聊天室！\n", Brushes.Red);
                    enter_command.Clear();
                    return;
                }
                    
                if (enter_command.Text != "")
                {
                    irc.sendchatMessage(enter_command.Text);
                    OriMsgQueue_Push("@MsgFromUSR,PRIVMSG@" + current_channel+".tmi :"+ enter_command.Text+"\n");
                    msg_reader.Interrupt();
                    enter_command.Clear();
                }
            }
        }

        private void keyin_Gotfocus(object sender, RoutedEventArgs e)       //游標點擊發話框
        {
            enter_command.Clear();

            Color color = (Color)ColorConverter.ConvertFromString("#d6d6d6");
            enter_command.Foreground = new System.Windows.Media.SolidColorBrush(color);
        }

        private void keyin_LostFocus(object sender, RoutedEventArgs e)      //游標點擊發話框外
        {
            Color color = (Color)ColorConverter.ConvertFromString("#505050");
            enter_command.Foreground = new System.Windows.Media.SolidColorBrush(color);
            enter_command.Text = "發話框";
        }

        private void WPF_DragLeave_Volume_Slider(object sender, MouseButtonEventArgs e)
        {
            Speech_Volume = Int32.Parse(WPF_Volume_slider_box.Text);
        }

        private void WPF_DragLeave_Rate_Slider(object sender, MouseButtonEventArgs e)
        {
            Speech_Rate = Int32.Parse(WPF_Rate_slider_box.Text);
        }



        private void Cheer_Mode_Click(object sender, RoutedEventArgs e)          //暫停說話
        {
            if (SPEAK_PAUSE == false)
            {
                SPEAK_PAUSE = true;
                Speak_Pause.Content = "恢復至一般模式";
                PutSystemMsg("===已進入Cheers模式===\n", Brushes.ForestGreen);
                Color temp = Color.FromArgb(255,184,110,0);
                TBwindow.NonActiveBorderBrush = new SolidColorBrush(temp);
                TBwindow.NonActiveGlowBrush = new SolidColorBrush(temp);
                TBwindow.BorderBrush = new SolidColorBrush(temp);
                TBwindow.GlowBrush = new SolidColorBrush(temp);
            }
            else
            {
                SPEAK_PAUSE = false;
                Speak_Pause.Content = "切換至Cheer棒讀模式";
                PutSystemMsg("===已離開Cheers模式===\n", Brushes.ForestGreen);
                Color temp = Color.FromArgb(255, 0, 76, 153);
                TBwindow.BorderBrush = new SolidColorBrush(temp);
                TBwindow.GlowBrush = new SolidColorBrush(temp);
                temp = Color.FromArgb(255, 102, 102, 102);
                TBwindow.NonActiveBorderBrush = new SolidColorBrush(temp);
                TBwindow.NonActiveGlowBrush = new SolidColorBrush(temp);
            }
        }

        private void Visit_TB_Blog(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        
        public static bool IsAutoLogin()
        {
            return AUTO_LOGIN;
        }

        public static void SetAutoLogin(bool set)
        {
            AUTO_LOGIN = set;
        }
        
        public static void Pitch_ValueChanged(string Pitch)  //棒讀音高:由進階設定視窗控制並回傳值
        {
            Speech_Pitch = Int32.Parse(Pitch);
        }

        public static void SE_Volume_slider_ValueChanged(string Se_Volume)    //音效音量:由進階設定視窗控制並回傳值
        {
            SE_Volume = (float.Parse(Se_Volume)) / 100;
        }

        public static void SpeechWordNum_Limit(string WordNum_Limits)   //棒讀字數限制:由進階設定視窗控制並回傳值
        {
            Speech_Word_Limit = Int32.Parse(WordNum_Limits);
        }

        public static void MinBitsSet(string Bits_Limit)    //最小Bits數量之棒讀:由進階設定視窗控制並回傳值
        {
            Min_Btis_Limit = Int32.Parse(Bits_Limit);
        }

        public static void SE_Cancel_Switch(bool _switch) //取消音效功能:由進階設定視窗控制並回傳值
        {
            SE_CANCEL_FUNC = _switch;
        }

        public static void Command_Speech_Switch(bool _switch)    //以!TB方式棒讀:由進階設定視窗控制並回傳值
        {
            COMMAND_SPEECH = _switch;
        }

        public static void HandleDoNotTalk_Switch(bool _switch)   //啟用防搶話:由進階設定視窗控制並回傳值
        {
            DoNotTalkFunction = _switch;
        }

        public static void SE_Pause_Switch(bool _switch)  //暫停SE功能:由進階設定視窗控制並回傳值
        {
            SE_PAUSE = _switch;
        }

        private void Detect_OS()
        {
            var os = Environment.OSVersion;
            switch (os.Version.Major)
            {
                case 10:
                    ISWIN7 = false;
                    PutSystemMsg("OS :" + os.VersionString + "\n", Brushes.LightGray);
                    break;
                case 6:
                    if (os.Version.Minor >= 2)
                    {
                        ISWIN7 = false;
                        PutSystemMsg("OS :" + os.VersionString + "\n", Brushes.LightGray);
                    }
                    else
                    {
                        ISWIN7 = true;
                        PutSystemMsg("OS Win7:" + os.VersionString + "\n", Brushes.LightGray);
                    }
                    break;
                default:
                    PutSystemMsg("非支援之系統?\n", Brushes.Red);
                    break;
            }
        }

        private void Open_AdSetWindow(object sender, RoutedEventArgs e)
        {
            AdvancedSetting st = new AdvancedSetting();
            st.Owner = this;
            st.ShowDialog();
        }

        public static int Get_Speech_Pitch()
        {
            return Speech_Pitch;
        }

        public static int Get_SE_Volume()
        {
            return (int)(SE_Volume * 100);
        }

        public static int Get_Speech_Word_Limit()
        {
            return Speech_Word_Limit;
        }

        public static int Get_Btis_Limit()
        {
            return Min_Btis_Limit;
        }

        public static bool Get_SE_CANCEL_FUNC()
        {
            return SE_CANCEL_FUNC;
        }

        public static bool Get_IsCommandSpeech()
        {
            return COMMAND_SPEECH;
        }

        public static bool Get_SPEAK_PAUSE()
        {
            return SPEAK_PAUSE;
        }

        public static bool Get_IsDoNotDisturb()
        {
            return DoNotTalkFunction;
        }


    }
    //===================End of MainWindow Class.======================
}
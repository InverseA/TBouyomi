using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.IO;
// System.Uri;
using System.Diagnostics;
using NAudio;
using NAudio.CoreAudioApi;
using MahApps.Metro.Controls;
using System.Collections;
using System.Linq;
using System.Collections.Concurrent;
using NAudio.Wave;

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
        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.033) };
        //DispatcherTimer Disturb_State_timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(6) };

        WaveIn Int_WaveIn = null;

        private string current_channel;
        private string current_OAuth;
        private IrcClient irc;

        private static Queue msg_qu = new Queue();
        private static Queue tts_qu = new Queue();
        Queue msg_queue = Queue.Synchronized(msg_qu);
        Queue TTS_queue = Queue.Synchronized(tts_qu);
        
        private bool DoNotTalkFunction = false; //控制防搶話是否開啟
        private bool SE_Cancel_Func = false;
        private static bool Auto_Login = false;
        private bool Disturb_State = false;
        private bool Speak_pause = false;
        private bool SE_pause = false;
        private bool CommandSpeech = false;

        //private static bool SE_Force_Enable = false;

        private int Speech_Volume = 100;
        private int Speech_Rate = 1;
        private int Speech_Pitch = 0;
        private int Speech_Word_Limit = 20;
        private float SE_Volume = 1;

        private static bool Mic_exist = false;
        private static int DoNotDisturbThreshold = 13;
        private Object thislock = new object();

        List<NickName> NickNameList;
        List<Education> EducationList;
        List<SE> SEList;
        List<ReAct> ReActList;
        List<Except> ExceptList; //排除棒讀的名單

        WaveStream MainOutputStream;
        WaveChannel32 volumeStream;
        WaveOutEvent player;

        Thread msg_reader = null;
        Thread Do_Not_Disturb = null;
        Thread IRCRoom = null;
        Thread TTS_T = null;
        Thread AutoPing = null;
        Thread GC_T = null;
        //Thread SE12 = null;
        //Thread SE30 = null;

        public MainWindow()
        {
            InitializeComponent();
            
            NickNameList = new List<NickName>();
            EducationList = new List<Education>();
            SEList = new List<SE>();
            ReActList = new List<ReAct>();
            ExceptList = new List<Except>();


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
            /*
            SE12 = new Thread(SE12_Thread);
            SE12.IsBackground = true;
            SE12.Start();

            SE30 = new Thread(SE30_Thread);
            SE30.IsBackground = true;
            SE30.Start();
            */
            DoNotDisturbInit();

            if (Auto_Login)
            {
                ReadLoginFile();
                StartSession();
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
                    Push_A_message_to_Room("您尚未登入聊天室！\n");
                    enter_command.Clear();
                    return;
                }
                    
                if (enter_command.Text != "")
                {
                    irc.sendchatMessage(enter_command.Text);
                    msg_queue.Enqueue("TextFormCommand!"+current_channel+"@BouyomiMessage:" + enter_command.Text+"\n");
                    Push_A_message_to_Room(current_channel + " : " + enter_command.Text + "\n");
                    enter_command.Clear();
                    msg_reader.Interrupt();
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

        private void WPF_DragLeave_NumLimit_Slider(object sender, MouseButtonEventArgs e)
        {
            Speech_Word_Limit = Int32.Parse(WordNum_Limitslider_box.Text);
        }

        private void HandleDoNotTalkCheck(object sender, RoutedEventArgs e)
        {
            DoNotTalkFunction = true;
        }

        private void HandleDoNotTalkUnchecked(object sender, RoutedEventArgs e)
        {
            DoNotTalkFunction = false;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)          //暫停說話
        {
            if (Speak_pause == false)
            {
                Speak_pause = true;
                Speak_Pause.Content = "繼續棒讀(Continue)";
            }
            else
            {
                Speak_pause = false;
                Speak_Pause.Content = "暫停棒讀(Pause)";
            }
        }

        private void Visit_TB_Blog(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        
        public static bool IsAutoLogin()
        {
            return Auto_Login;
        }

        public static void SetAutoLogin(bool set)
        {
            Auto_Login = set;
        }

        private void SE_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (SE_pause == false)
            {
                SE_pause = true;
                SE_Pause.Content = "恢復音效(SE Resume)";
            }
            else
            {
                SE_pause = false;
                SE_Pause.Content = "暫停音效(SE Pause)";
            }
        }

        private void SE_Cancel_Check(object sender, RoutedEventArgs e)
        {
            SE_Cancel_Func = true;
        }
        private void SE_Cancel_unCheck(object sender, RoutedEventArgs e)
        {
            SE_Cancel_Func = false;
        }

        private void SE_Volume_slider_ValueChanged(object sender, MouseButtonEventArgs e)
        {
            SE_Volume = (float.Parse(SE_Volume_box.Text)) / 100;
        }

        private void Command_Speech_Check(object sender, RoutedEventArgs e)
        {
            CommandSpeech = true;
        }

        private void Command_Speech_unCheck(object sender, RoutedEventArgs e)
        {
            CommandSpeech = false;
        }

        private void Pitch_ValueChanged(object sender, MouseButtonEventArgs e)
        {
            Speech_Pitch = Int32.Parse(Pitch_num_box.Text);
        }
    }
    //===================End of MainWindow Class.======================
}
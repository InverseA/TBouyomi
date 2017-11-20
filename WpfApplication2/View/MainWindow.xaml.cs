using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Collections;
using NAudio.CoreAudioApi;

using Google.Apis.YouTube.v3;
using Google.Apis.Services;


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

        private static string AppName = "Twitcco 1.7α";
        public static string Version = "1.7α";

        private static string current_channel;
        private static string current_OAuth;
        private static IrcClient irc;

        private static bool ISWIN7 = false;
        private static bool IsInit = true;
        private static bool ChannelChange_Flag = false;

        private static Queue original_msg_qu = new Queue();
        private static Queue tts_o_qu = new Queue();
        Queue OriMsg_object_queue = Queue.Synchronized(original_msg_qu);
        Queue TTS_object_queue = Queue.Synchronized(tts_o_qu);

        private static bool IS_ALREADY_LOGIN = false;

        private static string Default_Language = "TW";

        private static bool DoNotTalkFunction = false; //控制防搶話是否開啟
        private static bool ForceSlowMode = false;
        private static bool AutoSlowMode = true;
        private static bool SEOnlyMode = false; //音效模式
        private static bool AllowMODSE = false; //允許MOD使用指令暫時開啟音效
        private static bool SE_CANCEL_FUNC = false; //SE相互取消
        private static bool AUTO_LOGIN = false;
        private static bool DISTURB_STATE = false;
        private static bool SPEAK_PAUSE = false;
        private static bool SE_PAUSE = false;
        private static bool COMMAND_SPEECH = false;

        private static bool LIVE2D_CLIENT_EXIST = false;
        private static bool AUTO_START_LIVE2D = false;
        private static int LIVE2D_SPEAK_MODE = 0;   // 0 = 同步棒讀醬, 1 = 與使用者對嘴
        private static bool TBouyomiIsSpeak = false;

        private static string LIVE2D_USR_DEFAULT_CHARA = "Twitcco";
        private static int Speech_Volume = 100;
        private static int Speech_Rate = 1;
        private static int Speech_Pitch = 0;
        private static float SE_Volume = 1;
        private static int Speech_Word_Limit = 20;
        private static int Min_Btis_Limit = 1;

        private static bool MIC_EXIST = false;
        private static MMDevice MicDevice = null;
        private static int DoNotDisturbThreshold = 13;
        private static int VOLUME = 0;
        private static Object thislock = new object();

        List<NickName> NickNameList;
        static List<Education> EducationList;
        static List<SE> SEList;
        static List<ReAct> ReActList;
        static List<Except> ExceptList; //排除棒讀的名單
        static List<string> Live2Dfolder;

        Thread msg_reader = null;
        Thread Do_Not_Disturb = null;
        Thread IRCRoom = null;
        Thread TTS_T = null;
        Thread AutoPing = null;
        Thread ForceSlowModeController = null;
        Thread AutoSlowModeController = null;
        Thread AllowMODSEController = null;
        Thread GC_T = null;
        static Thread Live2D_T = null;
        

        public MainWindow()
        {
            InitializeComponent();
            SplashScreen splash = new SplashScreen("splash.jpg");
            splash.Show(true);
            Thread.Sleep(1000);
            NickNameList = new List<NickName>();
            EducationList = new List<Education>();
            SEList = new List<SE>();
            ReActList = new List<ReAct>();
            ExceptList = new List<Except>();
            Live2Dfolder = new List<string>();
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
            Read_All_Live2D_Chara();

            TTS_T = new Thread(SpeechQueue_Puller);
            TTS_T.IsBackground = true;
            TTS_T.Start();

            GC_T = new Thread(GC_Thread);
            GC_T.IsBackground = true;
            GC_T.Start();

            msg_reader = new Thread(Msg_Reader);
            msg_reader.IsBackground = true;
            msg_reader.Start();
            
            ForceSlowModeController = new Thread(ForceSlowModeCounter);
            ForceSlowModeController.IsBackground = true;
            ForceSlowModeController.Start();

            AutoSlowModeController = new Thread(AutoSlowModeCounter);
            AutoSlowModeController.IsBackground = true;
            AutoSlowModeController.Start();

            AllowMODSEController = new Thread(AllowMODSECounter);
            AllowMODSEController.IsBackground = true;
            AllowMODSEController.Start();

            DoNotDisturbInit();


            if (AUTO_LOGIN)
            {
                ReadLoginFile();
                try
                {
                    StartSession();
                }
                catch(Exception ex)
                {
                    PutSystemMsg("自動登入連線失敗 => StartSession()\n => " + ex.Message + "\n", Brushes.Red);
                }
            }

            IsInit = false;
            PutSystemMsg("此版本為內部測試版 1.7α\n", Brushes.LightGray);
            PutSystemMsg("主程序初始化完成\n", Brushes.Green);
            if (AUTO_START_LIVE2D)
            {
                Live2D_T = new Thread(OpenLive2DWindow);
                Live2D_T.IsBackground = true;
                Live2D_T.Start();
            }

            if (SE_PAUSE)
                ShowSE_PAUSEState(true);
            if (DoNotTalkFunction)
                ShowDoNotTalkState(true);
            if (AutoSlowMode)
                ShowAutoSlowModeState(true);
            if (ForceSlowMode)
                ShowForceSlowModeState(true);
            if (SE_CANCEL_FUNC)
                ShowSECancelState(true);
            if (SEOnlyMode)
                ShowSEOnlyModeState(true);
            if (AllowMODSE)
                ShowAllowMODSEState(true);

            if (LIVE2D_SPEAK_MODE == 0)
                ShowLive2D_SpeakMode("TB");
            else
                ShowLive2D_SpeakMode("Usr");

        }
    }
}
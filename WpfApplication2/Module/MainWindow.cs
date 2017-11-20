using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {

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

        //===== Deivce ====
        public bool GetMicExist()
        {
            return MIC_EXIST;
        }


        //===== Login ======

        public static bool IsAutoLogin()
        {
            return AUTO_LOGIN;
        }

        public static void SetAutoLogin(bool set)
        {
            AUTO_LOGIN = set;
        }
        
        //====== Bouyomi=======

        public static bool Get_SPEAK_PAUSE()
        {
            return SPEAK_PAUSE;
        }

        public static int Get_Bouyomi_Volume()
        {
            return Speech_Volume;
        }

        public static void Set_Bouyomi_Volume(string _volume)
        {
            Speech_Volume = Int32.Parse(_volume);
        }

        public static int Get_Speech_Rate()
        {
            return Speech_Rate;
        }

        public static void Set_Speech_Rate(string _Rate)
        {
            Speech_Rate = Int32.Parse(_Rate);
        }

        public static int Get_Speech_Pitch()
        {
            return Speech_Pitch;
        }

        public static void Pitch_ValueChanged(string Pitch)  //棒讀音高:由進階設定視窗控制並回傳值
        {
            Speech_Pitch = Int32.Parse(Pitch);
        }

        public static int Get_Speech_Word_Limit()
        {
            return Speech_Word_Limit;
        }

        public static void SpeechWordNum_Limit(string WordNum_Limits)   //棒讀字數限制:由進階設定視窗控制並回傳值
        {
            Speech_Word_Limit = Int32.Parse(WordNum_Limits);
        }

        public static bool Get_IsCommandSpeech()
        {
            return COMMAND_SPEECH;
        }

        public static void Command_Speech_Switch(bool _switch)    //以!TB方式棒讀:由進階設定視窗控制並回傳值
        {
            COMMAND_SPEECH = _switch;
        }

        public static string Get_DefaultLanguage()
        {
            return Default_Language;
        }

        public static void Set_DefaultLanguage(string _DL)
        {
            Default_Language = _DL;
        }

        //====== SE ======

        public int Get_SE_Volume()
        {
            return (int)(SE_Volume * 100);
        }

        public static void SE_Volume_slider_ValueChanged(string Se_Volume)    //音效音量:由進階設定視窗控制並回傳值
        {
            SE_Volume = (float.Parse(Se_Volume)) / 100;
        }
        
        public bool Get_SEPause()
        {
            return SE_PAUSE;
        }

        public void SE_Pause_Switch(bool _switch)  //暫停SE功能:由進階設定視窗控制並回傳值
        {
            SE_PAUSE = _switch;
        }

        public void ShowSE_PAUSEState(bool _state)
        {
            PutSystemMsg("禁止特殊音效：", Brushes.Green);
            if (_state)
                PutSystemMsg("啟用\n", Brushes.Green);
            else
                PutSystemMsg("關閉\n", Brushes.Green);
        }

        //====== Cheers ======

        public static int Get_Btis_Limit()
        {
            return Min_Btis_Limit;
        }

        public static void MinBitsSet(string Bits_Limit)    //最小Bits數量之棒讀:由進階設定視窗控制並回傳值
        {
            Min_Btis_Limit = Int32.Parse(Bits_Limit);
        }




        //====== Live2D ======

        public static bool Get_AUTO_START_LIVE2D_value()
        {
            return AUTO_START_LIVE2D;
        }

        public static void Set_Live2D_AutoStart(bool AutoStart)
        {
            AUTO_START_LIVE2D = AutoStart;
        }

        public static int Get_LIVE2D_SPEAK_MODE_value()
        {
            return LIVE2D_SPEAK_MODE;
        }

        public static List<string> GetAllCharaName()
        {
            return Live2Dfolder;
        }

        public static string Get_Live2D_DefaultChara()
        {
            return LIVE2D_USR_DEFAULT_CHARA;
        }

        public void Set_Live2D_SpeakMode(string SpeakMode)
        {
            if (SpeakMode == "TB")
                LIVE2D_SPEAK_MODE = 0;
            else if (SpeakMode == "Usr")
                LIVE2D_SPEAK_MODE = 1;
        }

        public void ShowLive2D_SpeakMode(string _Mode)
        {
            PutSystemMsg("動態角色：", Brushes.Green);
            if (_Mode == "Usr")
                PutSystemMsg("對應麥克風\n", Brushes.Green);
            else if (_Mode == "TB")
                PutSystemMsg("對應語音\n", Brushes.Green);
        }

        //====== ParameterInquire =======
        public List<Education> GetEducationList()
        {
            return EducationList;
        }

        public List<SE> GetSEList()
        {
            return SEList;
        }

        public List<ReAct> GetReActList()
        {
            return ReActList;
        }

        public List<Except> GetExceptList()
        {
            return ExceptList;
        }

        public void ReScanSEFolder()
        {
            SEList.Clear();
            GetSEFileList();
        }


        //====== Experiment ======

        public bool Get_IsDoNotDisturb()
        {
            return DoNotTalkFunction;
        }

        public bool Get_AutoSlowMode()
        {
            return AutoSlowMode;
        }

        public bool Get_ForceSlowMode()
        {
            return ForceSlowMode;
        }

        public bool Get_SECancel()
        {
            return SE_CANCEL_FUNC;
        }

        public bool Get_SEOnlyMode()
        {
            return SEOnlyMode;
        }

        public bool Get_AllowMODSE()
        {
            return AllowMODSE;
        }


        public void HandleDoNotTalk_Switch(bool _switch)   //啟用防搶話:由進階設定視窗控制並回傳值
        {
            DoNotTalkFunction = _switch;
        }

        public void ShowDoNotTalkState(bool _state)
        {
            PutSystemMsg("防搶話模式：", Brushes.Green);
            if (_state)
                PutSystemMsg("啟用\n", Brushes.Green);
            else
                PutSystemMsg("關閉\n", Brushes.Green);
        }

        public void HandleAutoSlowMode_Switch(bool _switch)
        {
            AutoSlowMode = _switch;
        }

        public void ShowAutoSlowModeState(bool _state)
        {
            PutSystemMsg("自動慢動作模式：", Brushes.Green);
            if (_state)
                PutSystemMsg("啟用\n", Brushes.Green);
            else
                PutSystemMsg("關閉\n", Brushes.Green);
        }

        public void WakeUpAutoSlowModeController()
        {
            lock (SlowModeInterruptLock)
            {
                SlowModeInterruptByUIClick = true;
            }
            AutoSlowModeController.Interrupt();
        }

        public void HandleForceSlowMode_Switch(bool _switch)
        {
            ForceSlowMode = _switch;
        }

        public void ShowForceSlowModeState(bool _state)
        {
            PutSystemMsg("強制慢動作模式：", Brushes.Green);
            if (_state)
                PutSystemMsg("啟用\n", Brushes.Green);
            else
                PutSystemMsg("關閉\n", Brushes.Green);
        }

        public void WakeUpForceSlowModeController()
        {
            lock (SlowModeInterruptLock)
            {
                SlowModeInterruptByUIClick = true;
            }
            ForceSlowModeController.Interrupt();
        }

        public void HandleSECancel_Switch(bool _switch)
        {
            SE_CANCEL_FUNC = _switch;
        }

        public void ShowSECancelState(bool _state)
        {
            PutSystemMsg("音效互相取消：", Brushes.Green);
            if (_state)
                PutSystemMsg("啟用\n", Brushes.Green);
            else
                PutSystemMsg("關閉\n", Brushes.Green);
        }

        public void HandleSEOnlyMode_Switch(bool _switch)
        {
            SEOnlyMode = _switch;
        }

        public void ShowSEOnlyModeState(bool _state)
        {
            PutSystemMsg("僅限音效模式：", Brushes.Green);
            if (_state)
                PutSystemMsg("啟用\n", Brushes.Green);
            else
                PutSystemMsg("關閉\n", Brushes.Green);
        }

        public void HandleAllowMODSE_Switch(bool _switch)
        {
            AllowMODSE = _switch;
        }

        public void ShowAllowMODSEState(bool _state)
        {
            PutSystemMsg("允許MOD使用指令暫時開啟棒讀活動：", Brushes.Green);
            if (_state)
                PutSystemMsg("啟用\n", Brushes.Green);
            else
                PutSystemMsg("關閉\n", Brushes.Green);
        }


        //====== About ======
        public string GetVersion()
        {
            return Version;
        }
    }
}

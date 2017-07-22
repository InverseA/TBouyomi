using System.Threading;
using NAudio.Wave;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void SpeechQueue_Puller()
        {
            TTS_object Package = null;
            string Msg = null;
            double _reTime;
            double _dTime;
            int _iTime;

            
            Thread IMT = null;
            IMT = new Thread(IgnoreMsgTip);
            IMT.IsBackground = true;
            IMT.Start();
            

            while (true)
            {
                try
                {
                    Thread.Sleep(3600000);
                }
                catch (ThreadInterruptedException)
                {
                    while(TTS_object_queue.Count != 0)
                    {
                        Package = SpeechQueue_pull();
                        Msg = Package.GetMsg();

                        switch (Package.GetType())
                        {
                            case "MSG": //============MSG=============
                                if (Package.IsPremium())
                                {
                                    SpeechTheText(Package);
                                    break;
                                }
                                else if ((!DISTURB_STATE) && (!SPEAK_PAUSE) && (TTS_object_queue.Count <=12))
                                {
                                    
                                    if(TTS_object_queue.Count >= 5)
                                        IMT.Interrupt();
                                        
                                    SpeechTheText(Package);
                                }
                                    
                                break;
                                
                            case "SE":  //============SE=============
                                if (Package.IsPremium())
                                {
                                    _reTime = PlaySE(Msg);
                                    if (_reTime > 0)
                                    {
                                        _dTime = _reTime * 1000;
                                        _iTime = (int)(_dTime - 10);
                                        if (!SE_CANCEL_FUNC)
                                        {
                                            if (_iTime <= 10)
                                                _iTime = 10;
                                            Thread.Sleep(_iTime);
                                        }
                                        else
                                            Thread.Sleep(10);
                                    }
                                    break;
                                }
                                else if ((!DISTURB_STATE) && (!SPEAK_PAUSE))
                                {
                                    if ((!SE_PAUSE) && (TTS_object_queue.Count < 16))
                                    {
                                        _reTime = PlaySE(Msg);
                                        if (_reTime > 0)
                                        {
                                            _dTime = _reTime * 1000;
                                            _iTime = (int)(_dTime - 10);
                                            if (!SE_CANCEL_FUNC)
                                            {
                                                if (_iTime <= 10)
                                                    _iTime = 10;
                                                Thread.Sleep(_iTime);
                                            }
                                            else
                                                Thread.Sleep(10);
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        SpeechTheText(Package);
                                        break;
                                    }
                                }
                                break;

                            default:    //============DEFAULT=============
                                break;
                        }//End switch
                        
                        Package = null;
                    }//End of (TTS_object_queue.Count) != 0
                }
            }
        }

        private void IgnoreMsgTip()
        {
            bool _FastMode = false;
            bool _IgnoreMode = false;
            while (true)
            {
                try
                {
                    if(TTS_object_queue.Count < 3)
                    {
                        if (_FastMode)
                            PutSystemMsg("呼...棒讀醬感到冷靜一些了\n", Brushes.DodgerBlue);
                        _FastMode = false;
                        _IgnoreMode = false;
                    }
                    Thread.Sleep(360000);
                }
                catch (ThreadInterruptedException)
                {
                    if (_FastMode == false)
                    {
                        PutSystemMsg("聊天室訊息過快，棒讀醬決定加速詠唱啦！\n", Brushes.DodgerBlue);
                        _FastMode = true;
                    }
                    if (TTS_object_queue.Count >= 8)
                    {
                        if (_IgnoreMode == false)
                        {
                            PutSystemMsg("棒讀醬進入賢者狀態啦！(忽略棒讀部分訊息)\n", Brushes.DodgerBlue);
                            _IgnoreMode = true;
                        }
                    }
                    Thread.Sleep(25000);
                }
            }
        }
    }
}

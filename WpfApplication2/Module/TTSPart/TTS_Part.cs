using System.Threading;
using NAudio.Wave;
using System.Windows.Media;
using System;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private static bool SlowModeInterruptByUIClick = false;
        private static bool SlowMode_MessagePass = true;
        private static bool JustSpeech = false;

        private static Object SlowModeInterruptLock = new object(); //For UI Event.
        private static Object SlowModeLock = new object();

        private void SpeechQueue_Puller()
        {
            TTS_object Package = null;
            string Msg = null;
            double _reTime;
            double _dTime;
            int _iTime;
            
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
                                if (SEOnlyMode)
                                    break;
                                if (Package.IsPremium())
                                {
                                    SpeechTheText(Package);
                                    break;
                                }
                                else if ((!DISTURB_STATE) && (!SPEAK_PAUSE))
                                {
                                    if (ForceSlowMode || AutoSlowMode)
                                    {
                                        if (SlowMode_MessagePass)
                                            SpeechTheText(Package);
                                        else
                                            break;
                                    }
                                    else
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
                                    if (ForceSlowMode || AutoSlowMode)
                                    {
                                        if (SlowMode_MessagePass)
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
                                            break;
                                    }
                                    else
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
                                }
                                break;

                            case "SentenceEnd":  //====SentenceEnd=====
                                if (ForceSlowMode && SlowMode_MessagePass == true)
                                {
                                    lock (SlowModeLock)
                                    {
                                        SlowMode_MessagePass = false;
                                        JustSpeech = true;
                                    }
                                    ForceSlowModeController.Interrupt();
                                    break;
                                }
                                else if (AutoSlowMode && SlowMode_MessagePass == true)
                                {
                                    lock (SlowModeLock)
                                    {
                                        JustSpeech = true;
                                    }
                                    AutoSlowModeController.Interrupt();
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

        private void ForceSlowModeCounter()
        {
            Random rnd = new Random(100000);
            int WaitTime = 0;
            while (true)
            {
                try
                {
                    if (!ForceSlowMode)
                        Thread.Sleep(3600000);
                    else
                    {
                        if (JustSpeech)
                        {
                            WaitTime = rnd.Next(6000, 10000);
                            Thread.Sleep(WaitTime);
                            lock (SlowModeLock)
                            {
                                JustSpeech = false;
                            }
                        }
                        else
                        {
                            lock (SlowModeLock)
                            {
                                SlowMode_MessagePass = true;
                            }
                            Thread.Sleep(600000);
                        }
                    }
                }
                catch (ThreadInterruptedException)
                {
                    if (SlowModeInterruptByUIClick)
                    {
                        lock (SlowModeLock)
                        {
                            SlowMode_MessagePass = true;
                        }
                        lock (SlowModeInterruptLock)
                        {
                            SlowModeInterruptByUIClick = false;
                        }
                    }
                }
            }
        }

        private void AutoSlowModeCounter()
        {
            Random rnd = new Random(100000);
            int WaitTime = 0;
            int SentenceCounter = 0;
            
            while (true)
            {
                try
                {
                    if (!AutoSlowMode || ForceSlowMode)
                        Thread.Sleep(3600000);
                    else
                    {
                        if (JustSpeech)
                        {
                            SentenceCounter++;

                            if (SentenceCounter > 1)    //當到達門檻
                            {
                                SentenceCounter = 1;
                                lock (SlowModeLock)
                                {
                                    SlowMode_MessagePass = false;
                                    JustSpeech = false;
                                }
                                WaitTime = rnd.Next(4500, 8000);
                                Thread.Sleep(WaitTime);
                                lock (SlowModeLock)
                                {
                                    SlowMode_MessagePass = true;
                                }
                                SentenceCounter = 0;
                            }
                            else
                            {
                                lock (SlowModeLock)
                                {
                                    JustSpeech = false;
                                }
                            }
                        }
                        else
                        {
                            WaitTime = rnd.Next(4000, 6000);
                            Thread.Sleep(WaitTime);
                            SentenceCounter--;

                            if (SentenceCounter < 0)
                            {
                                lock (SlowModeLock)
                                {
                                    SlowMode_MessagePass = true;
                                }
                                SentenceCounter = 0;
                                Thread.Sleep(600000);
                            }
                        }
                    }
                }
                catch (ThreadInterruptedException)
                {
                    if (SlowModeInterruptByUIClick)
                    {
                        lock (SlowModeLock)
                        {
                            SlowMode_MessagePass = true;
                        }
                        lock (SlowModeInterruptLock)
                        {
                            SlowModeInterruptByUIClick = false;
                        }
                    }
                }
            }
        }
    }
}

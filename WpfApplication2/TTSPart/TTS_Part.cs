using System.Threading;
using NAudio.Wave;

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
                                else if ((!DISTURB_STATE) && (!SPEAK_PAUSE) && (TTS_object_queue.Count <=25))
                                {
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
    }
}

using System;
using System.Threading;
using Microsoft.Speech.Synthesis;   //Win7 Ver.
//using System.Speech.Synthesis;    //Win8/10 Ver.
using NAudio.Wave;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void SpeechQueue_Puller()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(3600000);
                }
                catch (ThreadInterruptedException)
                {
                    while(TTS_queue.Count != 0)
                    {
                        string msg = TTS_queue.Dequeue().ToString();
                        int index = SE_index(msg);
                        if (!Disturb_State)
                        {
                            if ((!SE_pause) && index != -1)
                            {
                                double _dTime;
                                double _reTime;
                                int _iTime;

                                if (TTS_queue.Count <= 8)
                                {
                                    if (player != null)
                                    {
                                        player.Stop();
                                        player.Dispose();
                                        player = null;
                                        volumeStream.Dispose();
                                        volumeStream = null;
                                        MainOutputStream.Dispose();
                                        MainOutputStream = null;
                                    }

                                    _reTime = PlaySE(index);
                                    if (_reTime != 0)
                                    {
                                        _dTime = _reTime * 1000;
                                        _iTime = (int)(_dTime - 10);
                                        if (!SE_Cancel_Func)
                                        {
                                            if (_iTime <= 10)
                                                _iTime = 10;
                                            Thread.Sleep(_iTime);
                                        }
                                        else
                                            Thread.Sleep(10);
                                    }
                                }
                                else
                                {
                                    SpeechTheText(msg);
                                }
                            }
                            else
                            {
                                SpeechTheText(msg);
                            }
                        }// End of if (!Disturb_State)
                    }
                }
            }
        }

        private double PlaySE(int index)
        {
            string _SEPath = "SE";
            try
            {
                if (Speak_pause == true)
                    return 0;

                MainOutputStream = new Mp3FileReader(_SEPath + "/" + SEList[index].GetSEFileName());
                volumeStream = new WaveChannel32(MainOutputStream);
                
                player = new WaveOutEvent();
                player.Init(volumeStream);
                player.Volume = SE_Volume;
                player.Play();
                string[] _SETotalTime_S = volumeStream.TotalTime.ToString().Split(':');
                double _SETotalTime = 0;
                _SETotalTime += Double.Parse(_SETotalTime_S[0]) * 3600;
                _SETotalTime += Double.Parse(_SETotalTime_S[1]) * 60;
                _SETotalTime += Double.Parse(_SETotalTime_S[2]);
                
                return _SETotalTime;
            }
            catch (Exception ex)
            {
                Push_A_message_to_Room("synth.SelectVoice錯誤 Error:" + ex.Message + "\n");

                return -1;
            }
            
        }
        
        private void SpeechTheText(string msg)      //最後發音部分
        {
            //tps://msdn.microsoft.com/en-us/library/system.speech.synthesis.speechsynthesizer%28v=vs.110%29.aspx
            // Initialize a new instance of the SpeechSynthesizer.
            if (Speak_pause == true)
                return;
            SpeechSynthesizer synth = new SpeechSynthesizer();
            PromptBuilder pb = new PromptBuilder();

            // Configure the audio output. 
            //Win7 Verson.===================================
            
            SpeechSynthesizer synth_J = new SpeechSynthesizer();

            if (IsContainsJapanese(msg))
            {
                Speech_Rate = Speech_Rate - 2;
                try
                {
                    synth_J.SelectVoice("Microsoft Server Speech Text to Speech Voice (ja-JP, Haruka)");
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.SelectVoice錯誤 Error:" + ex.Message + "\n");
                }
            }
            else
            {
                try
                {
                    pb.ClearContent();
                    string prtn = "<voice name=\"Microsoft Server Speech Text to Speech Voice (zh-TW, HanHan)\"><prosody pitch=\"" + Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    pb.AppendSsmlMarkup(prtn);
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.SelectVoice錯誤 Error:" + ex.Message + "\n");
                }

            }
            
            //===============================================

            //Win8/10 Ver.===================================
            /*
            if (IsContainsJapanese(msg))
            {
                Speech_Rate = Speech_Rate - 2;
                try
                {
                    string prtn = "<voice name=\"Microsoft Haruka Desktop\"><prosody pitch=\"" + Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    pb.AppendSsmlMarkup(prtn);
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.SelectVoice錯誤 Error:" + ex.Message + "\n");
                }
            }
            else
            {
                try
                {
                    string prtn = "<voice name=\"Microsoft Hanhan Desktop\"><prosody pitch=\"" + Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    pb.AppendSsmlMarkup(prtn);
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.SelectVoice錯誤 Error:" + ex.Message + "\n");
                }
            }
            */
            //===============================================

            synth.Volume = Speech_Volume;

            if (TTS_queue.Count >= 8)            //依照Queue中的量控制說話速率
            {
                synth.Rate = 10;
            }
            else if (TTS_queue.Count >= 5)
            {
                synth.Rate = Speech_Rate + 5;
                if (synth.Rate > 10)
                {
                    synth.Rate = 10;
                }
            }
            else
            {
                if (Speech_Rate < -10)
                    synth.Rate = -10;
                else
                    synth.Rate = Speech_Rate;
            }
            
            // Speak the string.



            synth.SetOutputToDefaultAudioDevice();




            //Win7 Ver.======================================
            
            synth_J.SetOutputToDefaultAudioDevice();
            if (IsContainsJapanese(msg))
            {
                try
                {
                    synth_J.Speak(msg);
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.Speak()錯誤 Error:" + ex.Message + "\n");
                }
                Speech_Rate = Speech_Rate + 2;
            }
            else
            {
                try
                {
                    synth.Speak(pb);
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.Speak()錯誤 Error:" + ex.Message + "\n");
                }
            }

            synth_J.Dispose();
            
            //===============================================

            //Win8/10 Ver.===================================
            /*
            try
            {
                synth.Speak(pb);
                //synth.Speak(msg);
                //Push_A_message_to_Room(TTS_queue.Count + "\n");
            }
            catch (Exception ex)
            {
                Push_A_message_to_Room("synth.Speak()錯誤 Error:" + ex.Message + "\n");
            }

            if (IsContainsJapanese(msg))
                Speech_Rate = Speech_Rate + 2;
            */
            //===============================================


            synth.Dispose();
        }


    }
}

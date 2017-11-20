using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Speech_win8(TTS_object Package)
        {
            //tps://msdn.microsoft.com/en-us/library/system.speech.synthesis.speechsynthesizer%28v=vs.110%29.aspx
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                PromptBuilder pb = new PromptBuilder();
                int _Speech_Rate = Speech_Rate;
                int _Speech_Pitch = Speech_Pitch;
                if (Package.Get_Speech_Pitch() != -11)
                {
                    _Speech_Pitch = Package.Get_Speech_Pitch();
                }

                string msg = Package.GetMsg();

                msg = msg.Replace("<", "");
                msg = msg.Replace(">", "");
                msg = msg.Replace("\"", " ");
                msg = msg.Replace("\\", " ");
                msg = msg.Replace("/", " ");

                if (Package.IsJapanese())
                {
                    _Speech_Rate = _Speech_Rate - 2;
                    try
                    {
                        pb.ClearContent();
                        string prtn = "<voice name=\"Microsoft Haruka Desktop\"><prosody pitch=\"" + _Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                        pb.AppendSsmlMarkup(prtn);
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("synth.SelectVoice錯誤(日文) Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }
                else
                {
                    try
                    {
                        pb.ClearContent();
                        string prtn = null;
                        if (Default_Language == "TW")
                            prtn = "<voice name=\"Microsoft Hanhan Desktop\"><prosody pitch=\"" + _Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                        else if (Default_Language == "HK")
                            prtn = "<voice name=\"Microsoft Tracy Desktop\"><prosody pitch=\"" + _Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                        pb.AppendSsmlMarkup(prtn);
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("synth.SelectVoice錯誤(中文) Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }

                if(Package.GetSpeech_Volume() != -1)
                    synth.Volume = Package.GetSpeech_Volume();
                else
                    synth.Volume = Speech_Volume;

                if(Package.Get_Speech_Rate() != -11)
                    synth.Rate = Package.Get_Speech_Rate();
                else
                    synth.Rate = _Speech_Rate;

                synth.SetOutputToDefaultAudioDevice();

                try
                {
                    TBouyomiIsSpeak = true;
                    synth.Speak(pb);
                    TBouyomiIsSpeak = false;
                }
                catch (Exception ex)
                {
                    PutSystemMsg("synth.Speak()錯誤 Error:" + ex.Message + "\n" + msg + "\n", Brushes.DarkRed);
                }
            }

        }
    }
}

using System;
using Microsoft.Speech.Synthesis;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Speech_win7(TTS_object Package)
        {
            //tps://msdn.microsoft.com/en-us/library/system.speech.synthesis.speechsynthesizer%28v=vs.110%29.aspx
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();
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
                    synth.SelectVoice("Microsoft Server Speech Text to Speech Voice (ja-JP, Haruka)");
                }
                catch (Exception ex)
                {
                    PutSystemMsg("synth.SelectVoice(日文)錯誤 Error:" + ex.Message + "\n", Brushes.Red);
                }
            }
            else
            {
                try
                {
                    pb.ClearContent();
                    string prtn = null;
                    if (Default_Language == "TW")
                        prtn = "<voice name=\"Microsoft Server Speech Text to Speech Voice (zh-TW, HanHan)\"><prosody pitch=\"" + _Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    else if (Default_Language == "HK")
                        prtn = "<voice name=\"Microsoft Server Speech Text to Speech Voice (zh-TW, HunYee)\"><prosody pitch=\"" + _Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    
                    pb.AppendSsmlMarkup(prtn);
                }
                catch (Exception ex)
                {
                    PutSystemMsg("synth.SelectVoice(中文)錯誤 Error:" + ex.Message + "\n", Brushes.Red);
                }
            }

            if (Package.GetSpeech_Volume() != -1)
                synth.Volume = Package.GetSpeech_Volume();
            else
                synth.Volume = Speech_Volume;

            if (Package.Get_Speech_Rate() != -11)
                synth.Rate = Package.Get_Speech_Rate();
            else
                synth.Rate = _Speech_Rate;

            synth.SetOutputToDefaultAudioDevice();

            if (Package.IsJapanese())
            {
                try
                {
                    synth.Speak(msg);
                }
                catch (Exception ex)
                {
                    PutSystemMsg("synth.Speak()錯誤 Error:" + ex.Message + "\n", Brushes.Red);
                }
            }
            else
            {
                try
                {
                    TBouyomiIsSpeak = true;
                    synth.Speak(pb);
                    TBouyomiIsSpeak = false;
                }
                catch (Exception ex)
                {
                    PutSystemMsg("synth.Speak()錯誤 Error:" + ex.Message + "\n", Brushes.Red);
                }
            }

            synth.Dispose();
        }
    }
}

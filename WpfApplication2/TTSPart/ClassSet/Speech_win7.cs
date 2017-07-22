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
                    string prtn = "<voice name=\"Microsoft Server Speech Text to Speech Voice (zh-TW, HanHan)\"><prosody pitch=\"" + Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    pb.AppendSsmlMarkup(prtn);
                }
                catch (Exception ex)
                {
                    PutSystemMsg("synth.SelectVoice(中文)錯誤 Error:" + ex.Message + "\n", Brushes.Red);
                }
            }

            synth.Volume = Speech_Volume;
            if (!Package.IsPremium())
            {
                if (TTS_object_queue.Count >= 5)
                {
                    synth.Rate = _Speech_Rate + 3;
                    if (synth.Rate > 10)
                    {
                        synth.Rate = 10;
                    }
                }
                else
                {
                    if (_Speech_Rate < -10)
                        synth.Rate = -10;
                    else
                        synth.Rate = _Speech_Rate;
                }
            }

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
                    synth.Speak(pb);
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

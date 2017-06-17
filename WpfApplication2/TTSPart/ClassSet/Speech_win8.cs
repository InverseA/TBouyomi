using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Speech_win8(TTS_object Package)
        {
            //tps://msdn.microsoft.com/en-us/library/system.speech.synthesis.speechsynthesizer%28v=vs.110%29.aspx
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();
            PromptBuilder pb = new PromptBuilder();
            int _Speech_Rate = Speech_Rate;

            string msg = Package.GetMsg();
            if (Package.IsJapanese())
            {
                _Speech_Rate = _Speech_Rate - 2;
                try
                {
                    pb.ClearContent();
                    string prtn = "<voice name=\"Microsoft Haruka Desktop\"><prosody pitch=\"" + Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    pb.AppendSsmlMarkup(prtn);
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.SelectVoice錯誤(日文) Error:" + ex.Message + "\n");
                }
            }
            else
            {
                try
                {
                    pb.ClearContent();
                    string prtn = "<voice name=\"Microsoft Hanhan Desktop\"><prosody pitch=\"" + Speech_Pitch + "Hz\">" + msg + "</prosody ></voice>";
                    pb.AppendSsmlMarkup(prtn);
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("synth.SelectVoice錯誤(中文) Error:" + ex.Message + "\n");
                }
            }
            
            synth.Volume = Speech_Volume;
            if (!Package.IsPremium())
            {
                if (TTS_object_queue.Count >= 10)            //依照Queue中的量控制說話速率
                {
                    synth.Rate = 10;
                }
                else if (TTS_object_queue.Count >= 5)
                {
                    synth.Rate = _Speech_Rate + 5;
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

            try
            {
                synth.Speak(pb);
            }
            catch (Exception ex)
            {
                Push_A_message_to_Room("synth.Speak()錯誤 Error:" + ex.Message + "\n");
            }
            
            synth.Dispose();
        }
    }
}

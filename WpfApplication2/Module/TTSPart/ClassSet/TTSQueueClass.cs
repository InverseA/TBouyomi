using System;
using System.Collections.Generic;
using System.Linq;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void SpeechQueue_Push(string _msg, string _type, bool _premium)
        {
            TTS_object _tts_o = new TTS_object(_msg, _type, _premium);
            TTS_object_queue.Enqueue(_tts_o);
        }

        private TTS_object SpeechQueue_pull()
        {
            return (TTS_object)TTS_object_queue.Dequeue();
        }
    }

    
    class TTS_object
    {
        private bool _IsPremium = false;
        private bool _IsJapanese = false;
        private bool _IsCustomInfo = false;

        private string _Type = null;    //MSG, SE

        private string _SpeechMsg = null;
        private int _Speech_Volume = -1;    //0-100
        private int _Speech_Rate = -11;     //-10 - 10
        private int _Speech_Pitch = -11;    //-10 - 10
        private int _SE_Volume = 0;

        public TTS_object(string Msg, string Type, bool Premium)
        {
            _IsPremium = Premium;
            _Type = Type;
            _SpeechMsg = Msg;
            _IsJapanese = IsContainsJapanese(Msg);
            SetPersonalVolAndPitch();
        }

        public new string GetType()
        {
            return _Type;
        }

        public string GetMsg()
        {
            return _SpeechMsg;
        }

        public bool IsPremium()
        {
            return _IsPremium;
        }

        public void CustomInfo(int Speech_Volume, int Speech_Rate, int Speech_Pitch, int SE_Volume)
        {
            _Speech_Volume = Speech_Volume;
            _Speech_Rate = Speech_Rate;
            _Speech_Pitch = Speech_Pitch;
            _SE_Volume = SE_Volume;
            _IsCustomInfo = true;
        }

        public bool GetCustomInfo()
        {
            return _IsCustomInfo;
        }

        public int GetSpeech_Volume()
        {
            return _Speech_Volume;
        }

        public int Get_Speech_Rate()
        {
            return _Speech_Rate;
        }

        public int Get_Speech_Pitch()
        {
            return _Speech_Pitch;
        }

        public bool IsJapanese()
        {
            return _IsJapanese;
        }

        public void SetPersonalVolAndPitch()
        {
            if (_Type == "MSG")
            {
                string _TempMsg = _SpeechMsg;
                string[] _SplitMsg = _TempMsg.Split(')');
                for (int i = 0; i < _SplitMsg.Length; i++)
                {
                    if (_SplitMsg[i].StartsWith("音量(") || _SplitMsg[i].StartsWith("速度(") || _SplitMsg[i].StartsWith("音高("))
                    {
                        string[] _SubSplit = _SplitMsg[i].Split('(');
                        _TempMsg = _SubSplit[1];
                        if (_SubSplit[0] == "音量")
                        {
                            if (Int32.TryParse(_SubSplit[1], out _Speech_Volume))
                            {
                                if (_Speech_Volume > 100 || _Speech_Volume < 0)
                                    _Speech_Volume = -1;
                                else
                                    _SpeechMsg = _SpeechMsg.Replace(("音量(" + _SubSplit[1] + ")"), "");
                            }
                        }
                        else if (_SubSplit[0] == "速度")
                        {
                            if (Int32.TryParse(_SubSplit[1], out _Speech_Rate))
                            {
                                if (_Speech_Rate > 10 || _Speech_Rate < -10)
                                    _Speech_Rate = -11;
                                else
                                    _SpeechMsg = _SpeechMsg.Replace(("速度(" + _SubSplit[1] + ")"), "");
                            }
                        }
                        else if (_SubSplit[0] == "音高")
                        {
                            if(Int32.TryParse(_SubSplit[1], out _Speech_Pitch))
                            {
                                if (_Speech_Pitch > 10 || _Speech_Pitch < -10)
                                    _Speech_Pitch = -11;
                                else
                                    _SpeechMsg = _SpeechMsg.Replace(("音高("+ _SubSplit[1] + ")"), "");
                            }
                        }
                    }
                }
            }
        }

        private static bool IsContainsJapanese(string text)
        {
            if (text != null)
            {
                //var romaji = GetCharsInRange(text, 0x0020, 0x007E).Any();
                var hiragana = GetCharsInRange(text, 0x3040, 0x309F).Any(); //平仮名
                var katakana = GetCharsInRange(text, 0x30A0, 0x30FF).Any(); //片仮名
                                                                            //var kanji = GetCharsInRange(text, 0x4E00, 0x9FBF).Any();

                return (hiragana || katakana);
            }
            else
                return false;
        }

        private static IEnumerable<char> GetCharsInRange(string text, int min, int max)
        {
            return text.Where(e => e >= min && e <= max);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private string Msg_WordsNumLimit(string msg)
        {
            if (!IsOnlyletters(msg))
            {
                if (msg.Length > Speech_Word_Limit + 1)
                {
                    string s = "以下略\n";
                    msg = msg.Remove(Speech_Word_Limit);
                    msg = string.Concat(msg, s);
                }
            }
            else
            {
                string[] temp1;
                temp1 = msg.Split(' ');
                if (temp1.Length > Speech_Word_Limit)
                {
                    msg = msg.Remove(0);
                    for (int i = 0; i < Speech_Word_Limit; i++)
                    {
                        msg = string.Concat(msg, temp1[i] + " ");
                    }
                    msg = string.Concat(msg, "! rest snip");
                }
            }

            return msg;
        }
        
        private void SE_pro(string msg, bool _IsPremium)
        {
            int _index;

            if (msg == "")
                return;

            for (int i = 0; i < SEList.Count; i++)
            {
                _index = msg.IndexOf(SEList[i].GetWord());
                if (_index != -1)
                {
                    string _SubStringLeft = msg.Remove(_index).Replace("\n", "");
                    string _SubStringRight = msg.Remove(0, (_index + SEList[i].GetWord().Length)).Replace("\n", "");

                    SE_pro(_SubStringLeft, _IsPremium);

                    //TTS_object_queue.Enqueue(SEList[i].GetWord()); //original
                    SpeechQueue_Push(SEList[i].GetWord(), "SE", _IsPremium);  //test

                    SE_pro(_SubStringRight, _IsPremium);

                    return;
                }
            }

            //TTS_object_queue.Enqueue(msg); //original
            SpeechQueue_Push(msg, "MSG", _IsPremium);   //test
        }

        private static IEnumerable<char> GetCharsInRange(string text, int min, int max)
        {
            return text.Where(e => e >= min && e <= max);
        }

        public static bool IsContainsJapanese(string text)
        {
            if (text != null)
            {
                //var romaji = GetCharsInRange(text, 0x0020, 0x007E).Any();
                var hiragana = GetCharsInRange(text, 0x3040, 0x309F).Any(); //平仮名
                var katakana = GetCharsInRange(text, 0x30A0, 0x30FF).Any(); //片仮名
                //var kanji = GetCharsInRange(text, 0x4E00, 0x9FBF).Any();
                
                return (hiragana||katakana);
            }
            else
                return false;
        }

        public static bool IsOnlyletters(string msg)    //檢測訊息是否只包含羅馬字(英文訊息)
        {
            return Regex.IsMatch(msg, @"^[a-zA-Z0-9_\ \'\,\;\:\?\!\-\=\#\@\$\%\^\&\*\(\)\\\/\[\]\~\`\{\}]+$");
        }

    }
}

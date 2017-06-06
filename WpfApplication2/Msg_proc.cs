using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private string Who_Talk(string msg)     //擷取出該訊息是誰講的
        {
            string[] temp;
            int index;
            //the example from irc server is
            //:abcd!abcd@abcd.tmi.twitch.tv PRIVMSG #StreamerChannel : TestMessageFromListener
            string Talker = null;
            index = msg.IndexOf('!') + 1;
            msg = msg.Substring(index);

            temp = msg.Split('@');
            Talker = temp[0];

            return Talker;
        }


        private string Room_Content_Proccess(string msg)    //將訊息處理成一般聊天室的樣子(處理後不含傳訊息者的名字)
        {
            string Listener_msg = null;
            string[] temp;
            int index;
            //the example from irc server is
            //:abcd!abcd@abcd.tmi.twitch.tv PRIVMSG #StreamerChannel : TestMessageFromListener
            string Talker = null;
            index = msg.IndexOf('!') + 1;
            msg = msg.Substring(index);

            temp = msg.Split('@');
            Talker = temp[0];

            index = msg.IndexOf(':') + 1;
            Listener_msg = msg.Substring(index);
            //Push_A_message_to_Room(Talker + " : " + Listener_msg);
            return Listener_msg;
        }

        private string Speech_Content_Proccess(string msg)    //控制要發音的模式
        {
            if (msg.StartsWith("!") || msg.StartsWith("/"))
                return msg;

            if (msg.Contains("http:/") || msg.Contains("Http:/") || msg.Contains("HTTP:/") ||
                msg.Contains("https:/"))
            {
                msg = "有連結";
                return msg;
            }

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            if (msg.Contains("."))
            {
                if (!msg.StartsWith(".."))
                {
                    Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (reg.IsMatch(msg))
                    {
                        msg = "有連結";
                        return msg;
                    }
                }
            }
            msg = RepeatDetect(msg);
            msg = Emotion(msg);

            return msg;
        }

        private string RepeatDetect(string msg)
        {
            string temp = msg;
            temp = Regex.Replace(temp, @"[!！@＠#＃$＄%％\^︿&＆\*＊\(（\)）_＿\-－\+＋=＝\{\}｛｝\[\]［］＼\/／\;；\:：\'\’＂\?？\\\<＜\>＞\.\．\。\·\﹒\…︙\|｜\`\~‵～]", "");
            if (temp != "")
            {
                RepeatStringDetect ReProc;
                ReProc = new RepeatStringDetect(temp);
                if (ReProc.Repeated)
                {
                    temp = ReProc.AfterProc;
                    return temp;
                }
                else
                    return msg;
            }
            else
                return temp;
            


        }

        private string Emotion(string msg)  //表情偵測與替換
        {
            if (msg == "...\n")
            {
                msg = msg.Replace("...", "覺得無言");
            }
            while (msg.Contains("/") || msg.Contains("\\"))
            {
                msg = msg.Replace("/", "");
                msg = msg.Replace("\\", "");
            }
            while (msg.Contains("%"))
            {
                msg = msg.Replace("%", "啪");
            }
            while (msg.Contains("XD"))
            {
                msg = msg.Replace("XD", "表情大笑");
            }
            if (msg.Contains("0.0") || msg.Contains("o.o") || msg.Contains("0_0"))
            {
                msg = msg.Replace("0.0", "盯著你看");
                msg = msg.Replace("o.o", "盯著你看");
                msg = msg.Replace("0_0", "盯著你看");
            }
            if (msg.Contains("O.o") || msg.Contains("o.O") ||
                    msg.Contains("O_o") || msg.Contains("o_O"))
            {
                msg = msg.Replace("O.o", "有點驚訝");
                msg = msg.Replace("o.O", "有點驚訝");
                msg = msg.Replace("O_o", "有點驚訝");
                msg = msg.Replace("o_O", "有點驚訝");
            }
            if (msg.Contains("3.3"))
            {
                msg = msg.Replace("3.3", "表情看不太到");
            }
            if (msg.Contains("030") || msg.Contains("=3="))
            {
                msg = msg.Replace("030", "表情嘟嘴");
                msg = msg.Replace("=3=", "表情嘟嘴");
            }
            if (msg.Contains("= =") || msg.Contains("=.=") || msg.Contains("=_="))
            {
                msg = msg.Replace("= =", "表情無解");
                msg = msg.Replace("=.=", "表情無解");
                msg = msg.Replace("=_=", "表情無解");
            }
            while (msg.Contains("www") || msg.Contains("WWW") || msg.Contains("ｗｗｗ") || msg.Contains("ＷＷＷ"))
            {
                msg = msg.Replace("www", "walawala");
                msg = msg.Replace("WWW", "walawala");
                msg = msg.Replace("ｗｗｗ", "walawala");
                msg = msg.Replace("ＷＷＷ", "walawala");
            }
            while (msg.Contains("ww") || msg.Contains("WW") || msg.Contains("ｗｗ") || msg.Contains("ＷＷ"))
            {
                msg = msg.Replace("ww", "wala");
                msg = msg.Replace("WW", "wala");
                msg = msg.Replace("ｗｗ", "wala");
                msg = msg.Replace("ＷＷ", "wala");
            }
            while (msg.Contains("哈哈哈") || msg.Contains("呵呵呵") || msg.Contains("顆顆顆") || msg.Contains("嘻嘻嘻"))
            {
                msg = msg.Replace("哈哈哈", "哈");
                msg = msg.Replace("呵呵呵", "呵");
                msg = msg.Replace("顆顆顆", "顆");
                msg = msg.Replace("嘻嘻嘻", "嘻");
            }
            while (msg.Contains("~") || msg.Contains("～"))
            {
                msg = msg.Replace("~", "");
                msg = msg.Replace("～", "");
            }
            while (msg.Contains("^") || msg.Contains("︿"))
            {
                msg = msg.Replace("^", "");
                msg = msg.Replace("︿", "");
            }
            while (msg.Contains("BBBB") || msg.Contains("bbbb"))
            {
                msg = msg.Replace("BBBB", "");
                msg = msg.Replace("bbbb", "");
            }
            while (msg.Contains("DDDD") || msg.Contains("dddd"))
            {
                msg = msg.Replace("DDDD", "");
                msg = msg.Replace("dddd", "");
            }
            while (msg.Contains("GGGG") || msg.Contains("gggg"))
            {
                msg = msg.Replace("GGGG", "");
                msg = msg.Replace("gggg", "");
            }
            while (msg.Contains("QQQQ") || msg.Contains("qqqq"))
            {
                msg = msg.Replace("QQQQ", "");
                msg = msg.Replace("qqqq", "");
            }
            while (msg.Contains("RRRR") || msg.Contains("rrrr"))
            {
                msg = msg.Replace("RRRR", "");
                msg = msg.Replace("rrrr", "");
            }
            while (msg.Contains("881"))
            {
                msg = msg.Replace("881", "掰掰伊");
            }
            while (msg.Contains("8888"))
            {
                msg = msg.Replace("8888", "掰掰");
            }
            while (msg.Contains("777"))
            {
                msg = msg.Replace("777", "");
            }
            while (msg.Contains("666"))
            {
                msg = msg.Replace("666", "");
            }
            while (msg.Contains("878787"))
            {
                msg = msg.Replace("878787", "");
            }
            while (msg.Contains("444"))
            {
                msg = msg.Replace("444", "");
            }
            while (msg.Contains("333"))
            {
                msg = msg.Replace("333", "");
            }
            while (msg.Contains("222"))
            {
                msg = msg.Replace("222", "");
            }
            while (msg.Contains("111"))
            {
                msg = msg.Replace("111", "");
            }



            return msg;
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

        public static bool IsOnlyletters(string msg)
        {
            return Regex.IsMatch(msg, @"^[a-zA-Z0-9_\ \'\,\;\:\?\!\-\=\#\@\$\%\^\&\*\(\)\\\/\[\]\~\`\{\}]+$");
        }

    }
}

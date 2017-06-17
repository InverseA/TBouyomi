using System.Text.RegularExpressions;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void OriMsgQueue_Push(string _msg)
        {
            OrignalMsg_object _OriMsg_o = new OrignalMsg_object(_msg);
            OriMsg_object_queue.Enqueue(_OriMsg_o);
        }

        private OrignalMsg_object OriMsgQueue_Pull()
        {
            return (OrignalMsg_object)OriMsg_object_queue.Dequeue();
        }
    }

    class OrignalMsg_object
    {
        private bool _IsPremium = false;
        private string _talker = null;  //SetInfo()處理完後，為全英文小寫
        private string _msg = null;
        private string _BitsTotal = null;

        public OrignalMsg_object(string Talker, string Msg, bool IsPremium)
        {
            _talker = Talker;
            _msg = Msg;
            _IsPremium = IsPremium;
        }

        public OrignalMsg_object(string OriMsg)
        {
            SetInfo(OriMsg);    //處理talker,msg以及bits
        }

        //Class OrignalMsg_object 外部方法======
        public string GetTalker()
        {
            return _talker;
        }

        public string GetMsg()
        {
            return _msg;
        }
        
        public bool IsPremium()
        {
            return _IsPremium;
        }

        public string GetBitsTotal()
        {
            return _BitsTotal;
        }
        
        public void Speech_Msg_By_command()
        {
            if (_IsPremium)
            {
                if (_msg.StartsWith("!TB "))
                    _msg = _msg.Substring(4);
            }
            else
            {
                if (!_msg.StartsWith("!TB "))
                    _msg = "";
                else
                    _msg = _msg.Substring(4);
            }
        }

        public void Speech_Content_Proccess()    //控制要發音的模式
        {
            if (_msg.StartsWith("!") || _msg.StartsWith("/"))
            {
                return;
            }

            if (_msg.Contains("http:/") || _msg.Contains("Http:/") || _msg.Contains("HTTP:/") || _msg.Contains("https:/"))
            {
                _msg = "有連結";
                return ;
            }

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";

            if (_msg.Contains("."))
            {
                if (!_msg.StartsWith(".."))
                {
                    Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (reg.IsMatch(_msg))
                    {
                        _msg = "有連結";
                        return ;
                    }
                }
            }

            if (_IsPremium == false)
                _msg = RepeatDetect(_msg);
            _msg = Emotion(_msg);
        }

        public bool MsgIsCommand()
        {
            if (_msg.StartsWith("!") || _msg.StartsWith("/"))
                return true;
            else
                return false;
        }

        public void Msg_WordsNumLimit(int Speech_Word_Limit)
        {
            if (!IsOnlyletters(_msg))
            {
                if (_msg.Length > Speech_Word_Limit + 1)
                {
                    string s = "以下略\n";
                    _msg = _msg.Remove(Speech_Word_Limit);
                    _msg = string.Concat(_msg, s);
                }
            }
            else
            {
                string[] _temp1;
                _temp1 = _msg.Split(' ');
                if (_temp1.Length > Speech_Word_Limit)
                {
                    _msg = _msg.Remove(0);
                    for (int i = 0; i < Speech_Word_Limit; i++)
                    {
                        _msg = string.Concat(_msg, _temp1[i] + " ");
                    }
                    _msg = string.Concat(_msg, "! rest snip");
                }
            }
        }

        //Class OrignalMsg_object 內部方法======
        private string WhoTalk(string _OriMsg)
        {
            //the example from irc server is
            //:abcd!abcd@abcd.tmi.twitch.tv PRIVMSG #StreamerChannel : TestMessageFromListener
            string[] _temp;
            int _index;
            string _Talker = null;
            _index = _OriMsg.IndexOf('!') + 1;
            _OriMsg = _OriMsg.Substring(_index);

            _temp = _OriMsg.Split('@');
            _Talker = _temp[0];
            _Talker = _Talker.ToLower();

            return _Talker;
        }

        private string ChatMsg(string _OriMsg)  //抓取訊息，並判斷該訊息是否含有實質cheer
        {
            //the example from irc server is
            //:abcd!abcd@abcd.tmi.twitch.tv PRIVMSG #StreamerChannel : TestMessageFromListener
            string _Listener_msg = null;
            string[] _temp;
            int _index;
            _index = _OriMsg.IndexOf('!') + 1;
            _OriMsg = _OriMsg.Substring(_index);

            _temp = _OriMsg.Split('@');

            _index = _OriMsg.IndexOf(':') + 1;
            _Listener_msg = _OriMsg.Substring(_index);

            return _Listener_msg;
        }

        private string RepeatDetect(string _OriMsg)
        {
            string _temp = _OriMsg;
            _temp = Regex.Replace(_temp, @"[!！@＠#＃$＄%％\^︿&＆\*＊\(（\)）_＿\-－\+＋=＝\{\}｛｝\[\]［］＼\/／\;；\:：\'\’＂\?？\\\<＜\>＞\.\．\。\·\﹒\…︙\|｜\`\~‵～]", "");
            if (_temp != "")
            {
                RepeatStringDetect ReProc;
                ReProc = new RepeatStringDetect(_temp);
                if (ReProc.Repeated)
                {
                    _temp = ReProc.AfterProc;
                    return _temp;
                }
                else
                    return _OriMsg;
            }
            else
                return _temp;
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

        private static bool IsOnlyletters(string msg)    //檢測訊息是否只包含羅馬字(英文訊息)
        {
            return Regex.IsMatch(msg, @"^[a-zA-Z0-9_\ \'\,\;\:\?\!\-\=\#\@\$\%\^\&\*\(\)\\\/\[\]\~\`\{\}]+$");
        }

        private void SetInfo(string _OriMsg)
        {
            //@badges=staff/1,bits/1000;bits=100;color=;display-name=dallas;emotes=;id=b34ccfc7-4977-403a-8a94-33c6bac34fb8;mod=0;room-id=1337;subscriber=0;turbo=1;user-id=1337;user-type=staff :ronni!ronni@ronni.tmi.twitch.tv PRIVMSG #dallas :cheer100
            string _tag_info = null;
            string _temp = null;
            string[] _split_temp = null;
            int _index = 0;
            int _count = 0;

            _split_temp = _OriMsg.Split('@');
            _tag_info = _split_temp[1]; //儲存TAG訊息
            _temp = _split_temp[2];     //儲存Talker與留言訊息

            _index = _temp.IndexOf('.');
            _talker = _temp.Remove(_index).ToLower(); //儲存發送留言者
            _index = _temp.IndexOf(':') + 1;
            _msg = _temp.Substring(_index); //儲存留言訊息

            
            _count = 3;
            while(_count < _split_temp.Length)
            {
                _msg = string.Concat(_msg, "@"+_split_temp[_count]);
                _count++;
            }
            if (_tag_info.Contains("bits="))
            {
                _index = _tag_info.IndexOf("bits=");
                _index += 5;
                _temp = _tag_info.Substring(_index);

                _count = 0;
                while (_temp[_count] != ';')
                {
                    _count++;
                }
                _BitsTotal = _temp.Remove(_count);

                _IsPremium = true;
            }

            if (_IsPremium)
            {

            }
            
        }

    }
}

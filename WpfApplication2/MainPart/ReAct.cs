using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Add_ReAct(string word, string word_ra)
        {
            int index;
            string temp;

            index = ReAct_Index(word);

            if (index >= 0)
            {
                UpDateReAct(index, word_ra.Replace("\n", null));
            }
            else
            {
                ReAct NewReAct = new ReAct(word, word_ra.Replace("\n", null));
                ReActList.Add(NewReAct);
            }

            if (msg_reader != null && msg_reader.IsAlive)
            {
                temp = "自動回覆新增 : " + word.Replace("\n", "") + " > " + word_ra.Replace("\n", "");
                //msg_queue.Enqueue("TextFormCommand!" + current_channel + "@BouyomiMessage:" + "已學習字詞\n");
                Push_A_message_to_Room(temp + "\n");
                //msg_reader.Interrupt();
            }
        }

        private void UpDateReAct(int index, string word_ra)
        {
            ReActList[index].Update(word_ra);
        }

        private void Delete_ReAct(string word)
        {
            string temp;

            ReActList.RemoveAll(x => x.GetWord() == word);

            if (msg_reader != null && msg_reader.IsAlive)
            {
                temp = "刪除自動回覆 : " + word.Replace("\n", "");
                //msg_queue.Enqueue("TextFormCommand!" + current_channel + "@BouyomiMessage:" + temp + "\n");
                Push_A_message_to_Room(temp + "\n");
                //msg_reader.Interrupt();
            }
        }

        private int ReAct_Index(string word)
        {
            int index = 0;

            if (word != null)
            {
                for (int i = 0; i < ReActList.Count; i++)
                {
                    if (ReActList[i].GetWord().ToString() == word)
                        return index;
                    else
                        index++;
                }
            }
            return -1;
        }

        private void Msg_ReAct(string talker, string msg)
        {
            if (ReActList.Count != 0)
            {
                if (msg != null && talker != current_channel.ToLower())
                {
                    for (int i = 0; i < ReActList.Count; i++)
                    {
                        if ((ReActList[i].GetWord() != null) && (ReActList[i].Get_word_ra() != null))
                        {
                            if (msg.Contains(ReActList[i].GetWord()))
                            {
                                Push_A_message_to_Room(current_channel + " : " + ReActList[i].Get_word_ra() + "\n");
                                irc.sendchatMessage(ReActList[i].Get_word_ra());
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public class ReAct
    {
        private string word = null;
        private string word_ra = null;

        public ReAct(string Word, string word_ra)
        {
            this.word = Word.Replace("\n", null);
            this.word_ra = word_ra.Replace("\n", null);
        }

        public void Update(string word_ra)
        {
            this.word_ra = word_ra.Replace("\n", null);
        }

        public string GetWord()
        {
            return this.word;
        }
        public string Get_word_ra()
        {
            return this.word_ra;
        }
    }
}

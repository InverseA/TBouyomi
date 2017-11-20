using System;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        public void Add_ReAct(string word, string word_ra)
        {
            int index;
            index = ReAct_Index(word);
            try
            {
                word_ra = word_ra.Replace("\n", "");
                if (index >= 0)
                {
                    UpDateReAct(index, word_ra.Replace("\n", null));
                    PutSystemMsg("已更新自動回覆 : \n" + word + " ⇛ " + word_ra + "\n", Brushes.Green);
                }
                else
                {
                    ReAct NewReAct = new ReAct(word, word_ra.Replace("\n", null));
                    ReActList.Add(NewReAct);
                    if (!IsInit)
                        PutSystemMsg("已新增自動回覆 : \n" + word + " ⇛ " + word_ra + "\n", Brushes.Green);
                }
            }
            catch(Exception ex)
            {
                PutSystemMsg("Error 自動回覆 : " + ex.Message + "\n", Brushes.Red);
            }
            
        }

        private void UpDateReAct(int index, string word_ra)
        {
            ReActList[index].Update(word_ra);
        }

        public void Delete_ReAct(string word)
        {
            word = word.Replace("\n", "");
            try
            {
                ReActList.RemoveAll(x => x.GetWord() == word);
                PutSystemMsg("已刪除自動回覆 : " + word + "\n", Brushes.Green);
            }
            catch (Exception ex)
            {
                PutSystemMsg("Error 自動回覆 : " + ex.Message + "\n", Brushes.Red);
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
        public string word { get; set; }
        public string word_ra { get; set; }

        public ReAct(string Word, string word_ra)
        {
            this.word = Word;
            this.word_ra = word_ra;
        }

        public void Update(string word_ra)
        {
            this.word_ra = word_ra;
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

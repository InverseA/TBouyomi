using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Add_Education(string word, string word_ed)
        {
            int index;
            string temp;

            index = Education_Index(word);

            if(index >= 0)
            {
                UpDateEducation(index, word_ed.Replace("\n", null));
            }
            else
            {
                Education NewEdication = new Education(word, word_ed.Replace("\n", null));
                EducationList.Add(NewEdication);
            }
            
            if (msg_reader != null && msg_reader.IsAlive)
            {
                temp = "已學習字詞 : " + word.Replace("\n", "") + " = " + word_ed.Replace("\n", "");
                msg_queue.Enqueue("TextFormCommand!" + current_channel + "@BouyomiMessage:" + "已學習字詞\n");
                Push_A_message_to_Room(temp + "\n");
                msg_reader.Interrupt();
            }
        }

        private void UpDateEducation(int index, string Word_ed)
        {
            EducationList[index].Update(Word_ed);
        }

        private void Delete_Education(string word)
        {
            string temp;

            EducationList.RemoveAll(x => x.GetWord() == word);

            if (msg_reader != null && msg_reader.IsAlive)
            {
                temp = "已忘卻字詞 : " + word.Replace("\n", "");
                msg_queue.Enqueue("TextFormCommand!" + current_channel + "@BouyomiMessage:" + temp + "\n");
                Push_A_message_to_Room(temp + "\n");
                msg_reader.Interrupt();
            }
        }

        private int Education_Index(string word)
        {
            int index = 0;

            if(word != null)
            {
                for(int i = 0; i < EducationList.Count; i++)
                {
                    if (EducationList[i].GetWord().ToString() == word)
                        return index;
                    else
                        index++;
                }
            }
            return -1;
        }

        private string Education_String_Replace(string msg)
        {
            if(EducationList.Count != 0)
            {
                if (msg != null)
                {
                    for (int i = 0; i < EducationList.Count; i++)
                    {
                        if( (EducationList[i].GetWord() != null) && (EducationList[i].Get_Word_ed() != null) )
                            msg = msg.Replace(EducationList[i].GetWord(), EducationList[i].Get_Word_ed());
                    }
                }
            }

            return msg;
        }
    }

    public class Education
    {
        private string word = null;
        private string word_ed = null;

        public Education(string Word, string Word_ed)
        {
            this.word = Word.Replace("\n", null);
            this.word_ed = Word_ed.Replace("\n", null);
        }

        public void Update(string Word_ed)
        {
            this.word_ed = Word_ed.Replace("\n", null);
        }

        public string GetWord()
        {
            return this.word;
        }
        public string Get_Word_ed()
        {
            return this.word_ed;
        }
    }
}

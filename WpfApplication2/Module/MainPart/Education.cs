using System;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private static Object EducationLock = new object();
        public void Add_Education(string word, string word_ed)
        {
            int index;
            index = Education_Index(word);
            try
            {
                word_ed = word_ed.Replace("\n", "");
                if (index >= 0)
                {
                    lock (EducationLock)
                    {
                        UpDateEducation(index, word_ed);
                    }
                }
                else
                {
                    Education NewEdication = new Education(word, word_ed);
                    lock (EducationLock)
                    {
                        EducationList.Add(NewEdication);
                    }
                }
                if(!IsInit)
                    PutSystemMsg("已記住教育 : \n" + word + " ⇛ " + word_ed + "\n", Brushes.Green);
            }
            catch (Exception ex)
            {
                PutSystemMsg("Error 教育 : " + ex.Message + "\n", Brushes.Red);
            }
        }

        private void UpDateEducation(int index, string Word_ed)
        {
            EducationList[index].Update(Word_ed);
        }

        public void Delete_Education(string word)
        {
            try
            {
                lock (EducationLock)
                {
                    EducationList.RemoveAll(x => x.GetWord() == word);
                }
                PutSystemMsg("已忘記教育 : " + word + "\n", Brushes.Green);
            }
            catch (Exception ex)
            {
                PutSystemMsg("Error 教育 : " + ex.Message + "\n", Brushes.Red);
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

        private string Education_String_Replace(string msg, bool _Ispremium)
        {
            if (msg == null || msg == "")
                return msg;
            if(EducationList.Count != 0)
            {
                if (!_Ispremium)
                {
                    for (int i = 0; i < EducationList.Count; i++)
                    {
                        if ((EducationList[i].GetWord() != null) && (EducationList[i].Get_Word_ed() != null))
                            msg = msg.Replace(EducationList[i].GetWord(), EducationList[i].Get_Word_ed());
                    }
                }
                else
                {
                    for (int i = 0; i < EducationList.Count; i++)
                    {
                        while (msg.Contains(EducationList[i].GetWord()))
                        {
                            string _word = EducationList[i].GetWord();
                            int _index_front = msg.IndexOf(_word) + _word.Length;
                            int _index_rear = _index_front;
                            while (char.IsNumber(msg[_index_rear]))
                            {
                                _index_rear++;
                            }
                            msg= msg.Remove(_index_front,
                                        _index_rear - _index_front);
                            msg = msg.Replace(_word, EducationList[i].Get_Word_ed());

                        }
                    }
                }
            }
            return msg;
        }
    }

    public class Education
    {
        public string word { get; set; }
        public string word_ed { get; set; }

        public Education(string Word, string Word_ed)
        {
            this.word = Word;
            this.word_ed = Word_ed;
        }

        public void Update(string Word_ed)
        {
            this.word_ed = Word_ed;
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

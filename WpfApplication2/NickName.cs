using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Add_NickName(string talker, string talker_NK)
        {
            int index;
            index = Nick_Name_Index(talker);
            if (index >= 0)
            {
                UpDateNickName(index, talker_NK.Replace("\n", null));
            }
            else
            {
                NickName NewNickName = new NickName(talker, talker_NK.Replace("\n", null));
                NickNameList.Add(NewNickName);
            }
        }

        private void Delete_NickName(string talker)
        {
            //NickName NewNickName = new NickName(talker, TransToNick(talker));
            NickNameList.RemoveAll(x => x.GetTalker() == talker);
        }

        private void UpDateNickName(int index, string NickName)
        {
            NickNameList[index].Update(NickName);
        }

        private bool check_Ncik(string talker)
        {
            if (Nick_Name_Index(talker) >= 0)
            {
                return true;
            }
            else
                return false;
        }

        private int Nick_Name_Index(string talker)
        {
            int index = 0;

            if(talker != null)
            {
                for (int i = 0; i < NickNameList.Count; i++)
                {
                    if (NickNameList[i].GetTalker().ToString() == talker)
                        return index;
                    else
                        index++;
                }
            }
            return -1;
            //return (NickNameList.FindIndex(a => a.GetTalker() == talker)); //tp://stackoverflow.com/questions/5419278/get-index-of-an-object-in-a-generic-list
        }

        private string TransToNick(int index)
        {
            return (NickNameList[index].Get_Talker_NK().ToString()
                        .Replace(System.Environment.NewLine, ""));
        }

        private string TransToNick(string talker)
        {
            return (NickNameList[Nick_Name_Index(talker)].Get_Talker_NK().ToString()
                        .Replace(System.Environment.NewLine, ""));
        }

        private void List_All_Nick()
        {
            if (NickNameList.Count > 0)
            {
                for (int i = 0; i < NickNameList.Count; i++)
                    Push_A_message_to_Room(NickNameList[i].GetTalker() + " " + NickNameList[i].Get_Talker_NK() + "\n");
            }
        }
    }


    public class NickName
    {
        private string talker = null;
        private string talker_NK = null;

        public NickName(string Talker, string Talker_NK)
        {
            this.talker = Talker.Replace("\n",null);
            this.talker_NK = Talker_NK.Replace("\n", null);
        }

        public void Update(string Talker_NK)
        {
            this.talker_NK = Talker_NK.Replace("\n", null);
        }
        
        public string GetTalker()
        {
            return this.talker;
        }

        public string Get_Talker_NK()
        {
            return this.talker_NK;
        }
    }
}

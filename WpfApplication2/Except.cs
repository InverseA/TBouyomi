using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Add_Except(string name)
        {
            //int index;
            string temp;
            
            Except NewExcept = new Except(name.Replace("\n", null));
            ExceptList.Add(NewExcept);

            if (msg_reader != null && msg_reader.IsAlive)
            {
                temp = "已忽略帳號 : " + name.Replace("\n", "");
                msg_queue.Enqueue("TextFormCommand!" + current_channel + "@BouyomiMessage:" + "已忽略帳號\n");
                Push_A_message_to_Room(temp + "\n");
                msg_reader.Interrupt();
            }
        }
        
        private void Delete_Except(string name)
        {
            string temp;

            ExceptList.RemoveAll(x => x.GetName() == name);

            if (msg_reader != null && msg_reader.IsAlive)
            {
                temp = "已取消忽略 : ";
                msg_queue.Enqueue("TextFormCommand!" + current_channel + "@BouyomiMessage:" + temp + "\n");
                Push_A_message_to_Room(temp + "\n");
                msg_reader.Interrupt();
            }
        }

        private int Except_Index(string name)
        {
            if (ExceptList == null)
                return -1;
            int index = 0;

            if (name != "" || name != null)
            {
                for (int i = 0; i < ExceptList.Count; i++)
                {
                    if (ExceptList[i].GetName().ToString() == name)
                        return index;
                    else
                        index++;
                }
            }
            return -1;
        }
        /*
        private string Except_String_Replace(string msg)
        {
            if (ExceptList.Count != 0)
            {
                if (msg != null)
                {
                    for (int i = 0; i < ExceptList.Count; i++)
                    {
                        if ((ExceptList[i].Getname() != null) && (ExceptList[i].Get_name_ed() != null))
                            msg = msg.Replace(ExceptList[i].Getname(), ExceptList[i].Get_name_ed());
                    }
                }
            }

            return msg;
        }
        */
    }

    





class Except
    {
        private string name = null;

        public Except(string name)
        {
            this.name = name.Replace("\n", null);
        }
        /*
        public void Update(string name_ed)
        {
            this.name_ed = name_ed.Replace("\n", null);
        }
        */

        public string GetName()
        {
            return this.name;
        }
        /*
        public string Get_name_ed()
        {
            return this.name_ed;
        }
        */
    }
}

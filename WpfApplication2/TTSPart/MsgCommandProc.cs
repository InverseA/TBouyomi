﻿using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Msg_Command_Proc(OrignalMsg_object package)
        {
            string msg = package.GetMsg();
            string talker = package.GetTalker();

            //============= NickName Command Called by listener ============
            if (msg.StartsWith("!暱稱 "))
            {
                int index = 0;
                string temp = null;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index);
                Add_NickName(talker, temp);
            }
            if (msg == "!忘卻\n")
            {
                Delete_NickName(talker);
            }
            if (msg == "!查詢暱稱\n")
            {
                List_All_Nick();
            }


            //============= Education Command Called by listener ============
            if (msg.StartsWith("!教育 "))
            {
                int index = 0;
                string temp = null;
                string[] sub_msg;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index);
                temp = temp.Replace(" ", "");
                temp = temp.Replace("\n", "");
                if (temp != null)
                {
                    sub_msg = temp.Split('=');
                    if (sub_msg.Length == 2)
                    {
                        if (sub_msg[0] != "")
                        {
                            Add_Education(sub_msg[0], sub_msg[1]);
                        }
                        else
                        {
                            PutSystemMsg("有人試圖調教棒讀醬，但失敗啦！(注意格式是否有誤)\n", Brushes.MediumOrchid);
                        }
                    }
                    else
                    {
                        PutSystemMsg("有人試圖調教棒讀醬，但失敗啦！(注意格式是否有誤)\n", Brushes.MediumOrchid);
                    }
                }
                else
                {
                    PutSystemMsg("有人試圖調教棒讀醬，但失敗啦！(注意格式是否有誤)\n", Brushes.MediumOrchid);
                }
            }//End if(教育)
            if (msg.StartsWith("!忘卻 "))
            {
                int index = 0;
                string temp = null;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index).Replace("\n", "");
                if (temp != "")
                {
                    Delete_Education(temp);
                }
            }//End if(忘卻)

            //=======回覆============
            if (msg.StartsWith("!回覆 ") && talker == current_channel.ToLower())
            {
                int index = 0;
                string temp = null;
                string[] sub_msg;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index);
                temp = temp.Replace(" ", "");
                temp = temp.Replace("\n", "");
                if (temp != null)
                {
                    sub_msg = temp.Split('>');
                    if (sub_msg.Length == 2)
                    {
                        if (sub_msg[0] != "")
                        {
                            Add_ReAct(sub_msg[0], sub_msg[1]);
                        }
                        else
                        {
                            PutSystemMsg("有人試圖調教棒讀醬，但失敗啦！(注意格式是否有誤)\n", Brushes.MediumOrchid);
                        }
                    }
                    else
                    {
                        PutSystemMsg("有人試圖調教棒讀醬，但失敗啦！(注意格式是否有誤)\n", Brushes.MediumOrchid);
                    }
                }
                else
                {
                    PutSystemMsg("有人試圖調教棒讀醬，但失敗啦！(注意格式是否有誤)\n", Brushes.MediumOrchid);
                }
            }//End if(回覆)
            if (msg.StartsWith("!刪除回覆 ") && talker == current_channel.ToLower())
            {
                int index = 0;
                string temp = null;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index).Replace("\n", "");
                if (temp != "")
                {
                    Delete_ReAct(temp);
                }
            }//End if(刪除回覆)

            //=======忽略帳號=========
            if (msg.StartsWith("!忽略 ") && talker == current_channel.ToLower())
            {
                int index = 0;
                string temp = null;
                //string[] sub_msg;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index);
                temp = temp.Replace(" ", "");
                temp = temp.Replace("\n", "");
                if (temp != null && temp != "")
                {
                    temp = temp.ToLower();
                    Add_Except(temp);
                }
                else
                {
                    PutSystemMsg("忽略帳號命令錯誤：注意格式是否有誤\n", Brushes.MediumOrchid);
                }
            }//End if(教育)
            if (msg.StartsWith("!取消忽略 ") && talker == current_channel.ToLower())
            {
                int index = 0;
                string temp = null;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index).Replace("\n", "");
                if (temp != "")
                {
                    Delete_Except(temp);
                }
            }//End if(忘卻)

            //=======Notice==========
            
            if (msg.StartsWith("!提醒 ") && package.IsMod())
            {
                int index = 0;
                string temp = null;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index).Replace("\n", "");

                if (temp != "")
                {
                    NoticeUsr();
                    PutSystemMsg("⚔Mod提醒訊息 : " + temp + "\n", Brushes.LimeGreen);
                }
            }
            
            //======= MOD ===========
            /*
            if (msg.StartsWith("!SE12"))
            {
                irc.SendModListRequest();



            }
            
            if (msg.StartsWith("!SE30"))
            {
                
            }
            */
        }
        
    }
}
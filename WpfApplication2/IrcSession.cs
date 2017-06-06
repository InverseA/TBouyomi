using System;
using System.Threading;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void StartSession()
        {
            string msg;
            current_channel = current_channel.ToLower();
            irc = new IrcClient("irc.twitch.tv", 6667, current_channel, current_OAuth);
            msg = irc.readMessage();
            //=========Debug=========
            //Push_A_message_to_Room(msg);
            //=======================
            if (msg.Contains("Login authentication failed"))
            {
                Push_A_message_to_Room("登入失敗，帳號或密碼錯誤？\n");
            }
            else
            {
                current_ID_text.Content = current_channel;
                IRCRoom = new Thread(Enter_IRC_Room);
                IRCRoom.IsBackground = true;    //如此使得main thread 結束後子thread 也可以跟著被關閉.
                IRCRoom.Start();
                AutoPing = new Thread(IRCRoom_AutoPing);
                AutoPing.IsBackground = true;   //如此使得main thread 結束後子thread 也可以跟著被關閉.
                AutoPing.Start();

                //顯示觀看者數的功能
                
                Thread Viewer = new Thread(Channel_viewer);
                Viewer.IsBackground = true;
                Viewer.Start();
                
                
            }
        }

        private void Msg_Reader()
        {
            string msg;
            string talker = null;

            while (true)
            {
                try
                {
                    Thread.Sleep(3600000);
                }
                catch (ThreadInterruptedException)
                {
                    while (msg_queue.Count != 0)
                    {
                        msg = msg_queue.Dequeue().ToString();
                        talker = Who_Talk(msg);
                        talker = talker.ToLower();

                        msg = Room_Content_Proccess(msg);   //回傳的msg已經處理成僅剩留言訊息，不包含talker

                        if (CommandSpeech)                  //如果以指令來說話之功能 啟用
                        {
                            msg = Speech_Msg_By_command(msg);
                        }
                        msg = Speech_Content_Proccess(msg); //處理多餘的符號

                        Msg_Command_Proc(talker, msg);

                        if (!msg.StartsWith("!") && !msg.StartsWith("/"))   //如果不是指令
                        {
                            if (Except_Index(talker) == -1)   //如果不是Bot
                            {
                                int index = Nick_Name_Index(talker);    //判別該留言之人是否具有暱稱
                                if (index >= 0) //假如有暱稱
                                {
                                    string _NickName;
                                    _NickName = TransToNick(index);
                                    TTS_queue.Enqueue(_NickName);
                                    TTS_queue.Enqueue("說");
                                }

                                msg = Education_String_Replace(msg);
                                msg = Msg_WordsNumLimit(msg);
                                //====此時的msg已是替換過教育文字並被限制字數後的訊息====
                                msg = msg.Replace("\n", "");
                                if (!SE_pause)
                                    SE_pro(msg);    //使用recursive之方式
                                else
                                    TTS_queue.Enqueue(msg);
                                TTS_T.Interrupt();


                                //====自動回應====

                                Msg_ReAct(talker, msg);

                                if (msg.Contains("やっほー"))  //檢查棒讀醬反應
                                {
                                    if (talker != current_channel.ToLower())
                                    {
                                        Push_A_message_to_Room(current_channel + " : Ver 1.3.1 やっほー\n");
                                        irc.sendchatMessage("Ver 1.3.1 やっほー");
                                    }
                                }
                                
                                //================
                            }
                        }//End_ else if(!msg.StartsWith("!"))
                    }//End_ while(msg_queue.Count != 0)

                }//End_ catch
            }
        }

        //用thread的方式建立，並與Twitch伺服器做溝通.
        private void Enter_IRC_Room()       //IRC聊天室的主控制程序
        {
            string talker = null;
            string msg = null;

            Push_A_message_to_Room("***Ver 1.3.1 START.\n");
            Push_A_message_to_Room("***已登入Twitch IRC Room.\n");
            try
            {
                SpeechTheText("棒讀開始");
                Push_A_message_to_Room("***棒讀開始。\n");
            }
            catch (Exception ex)
            {
                Push_A_message_to_Room("第1句話失敗 Error : " + ex.Message + "\n");
            }
            while (true)
            {
                msg = null;
                try
                {
                    msg = irc.readMessage();
                }
                catch(Exception ex)
                {
                    
                }

                //===============Debug=================
                //Push_A_message_to_Room(msg);
                //=====================================

                if (msg.StartsWith("PING"))
                {
                    irc.sendMessage("PONG tmi.twitch.tv\r\n");
                }
                else
                {
                    if (msg.Contains("RIVMSG"))
                    {
                        talker = Who_Talk(msg);
                        talker = talker.ToLower();
                        
                        Push_A_message_to_Room(talker + " : " + Room_Content_Proccess(msg));
                        msg_queue.Enqueue(msg);     //MsgQueue放入接收的訊息

                        msg_reader.Interrupt();
                    }
                }

            }
        }

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

        private string Speech_Msg_By_command(string msg)
        {
            if(!msg.StartsWith("!TB "))
            {
                msg = "";
            }
            else
            {
                msg = msg.Substring(4);
            }
            return msg;
        }

        private void Msg_Command_Proc(string talker, string msg)
        {
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
                            //irc.sendchatMessage("教育失敗...請注意格式使否有誤(´・ω・`)");
                            Push_A_message_to_Room(current_channel + " : 教育失敗...請注意格式使否有誤\n");
                        }
                    }
                    else
                    {
                        //irc.sendchatMessage("教育失敗...請注意格式使否有誤(´・ω・`)");
                        Push_A_message_to_Room(current_channel + " : 教育失敗...請注意格式使否有誤\n");
                    }
                }
                else
                {
                    //irc.sendchatMessage("教育失敗...請注意格式使否有誤(´・ω・`)");
                    Push_A_message_to_Room(current_channel + " : 教育失敗...請注意格式使否有誤\n");
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
            if (msg.StartsWith("!回覆 ") && talker==current_channel.ToLower())
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
                            Push_A_message_to_Room(current_channel + " : 自動回覆設定失敗...請注意格式使否有誤\n");
                        }
                    }
                    else
                    {
                        Push_A_message_to_Room(current_channel + " : 自動回覆設定失敗...請注意格式使否有誤\n");
                    }
                }
                else
                {
                    Push_A_message_to_Room(current_channel + " : 自動回覆設定失敗...請注意格式使否有誤\n");
                }
            }//End if(教育)
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
            }//End if(忘卻)

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
                    Push_A_message_to_Room(current_channel + " : 自動回覆設定失敗...請注意格式使否有誤\n");
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
            /*
            if (msg.StartsWith("!提醒 "))
            {
                int index = 0;
                string temp = null;

                index = msg.IndexOf(" ") + 1;
                temp = msg.Substring(index).Replace("\n", "");

                if (temp != "")
                {
                    if (Disturb_State)
                    {
                        SpeechTheText(temp);
                    }
                }
            }
            */
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

        private void IRCRoom_AutoPing()
        {
            while (true)
            {
                Thread.Sleep(180000);
                irc.sendMessage("PING tmi.twitch.tv\r\n");
            }
        }
        /*
        private bool IsMod(string talker, string[] Talker_List)
        {
            for(int i = 0; i < Talker_List.Length; i++)
            {
                if (Talker_List[i].Replace(" ", "") == talker)
                    return true;
            }
            return false;
        }

        private void SE12_Thread()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(3600000);
                }
                catch (Exception)
                {
                    
                    Thread.Sleep(1500);
                    if(IsMod())
                    SE_Force_Enable = true;
                    Thread.Sleep(12000);
                    SE_Force_Enable = false;
                }
            }
        }

        private void SE30_Thread()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(3600000);
                }
                catch (Exception)
                {
                    SE_Force_Enable = true;
                    Thread.Sleep(30000);
                    SE_Force_Enable = false;
                }
            }
        }
        */
        private void SE_pro(string msg)
        {
            int _index;

            if (msg == "")
                return;
            
            for(int i = 0; i < SEList.Count; i++)
            {
                _index = msg.IndexOf(SEList[i].GetWord());
                if(_index != -1)
                {
                    string _SubStringLeft = msg.Remove(_index).Replace("\n", "");
                    string _SubStringRight = msg.Remove(0, (_index + SEList[i].GetWord().Length)).Replace("\n", "");

                    SE_pro(_SubStringLeft);
                    TTS_queue.Enqueue(SEList[i].GetWord());
                    SE_pro(_SubStringRight);

                    return;
                }
            }

            TTS_queue.Enqueue(msg);
        }
    }
}

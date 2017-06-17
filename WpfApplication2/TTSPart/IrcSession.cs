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
                /*
                Thread Viewer = new Thread(Channel_viewer);
                Viewer.IsBackground = true;
                Viewer.Start();
                */
            }
        }

        private void Msg_Reader()
        {
            OrignalMsg_object Package = null;
            string Talker = null;
            string Msg = null;
            while (true)
            {
                try
                {
                    Thread.Sleep(3600000);
                }
                catch (ThreadInterruptedException)
                {
                    while (OriMsg_object_queue.Count != 0)
                    {
                        Package = OriMsgQueue_Pull();
                        Push_A_message_to_Room(">"+Package.GetTalker() + " : " + Package.GetMsg());

                        if (COMMAND_SPEECH)                  //如果以指令"!TB"來說話之功能 啟用
                            Package.Speech_Msg_By_command();
                        
                        Package.Speech_Content_Proccess();  //處理多餘的符號
                        Talker = Package.GetTalker();
                        Msg = Package.GetMsg();

                        if (Package.MsgIsCommand() == true) //如果是指令
                            Msg_Command_Proc(Talker, Msg);  //判別並處理命令
                        else
                        {
                            if (Msg.Contains("@"))
                            {
                                if(!Package.IsPremium())
                                    continue;
                            }
                            if (Except_Index(Talker) == -1)   //排除名單:如果不是Bot
                            {
                                int _index = Nick_Name_Index(Talker);    //判別該留言帳號是否具有暱稱
                                if (_index >= 0) //假如有暱稱
                                {
                                    string _NickName;
                                    _NickName = TransToNick(_index);
                                    SpeechQueue_Push(_NickName, "MSG", Package.IsPremium());
                                    SpeechQueue_Push("說", "MSG", Package.IsPremium());
                                }

                                Msg = Education_String_Replace(Msg);
                                Msg = Msg_WordsNumLimit(Msg);

                                //====此時的msg已是替換過教育文字並被限制字數後的訊息====
                                Msg = Msg.Replace("\n", "");
                                if (!SE_PAUSE)
                                    SE_pro(Msg, Package.IsPremium());    //使用recursive之方式
                                else
                                    SpeechQueue_Push(Msg, "MSG", Package.IsPremium());
                                TTS_T.Interrupt();


                                //====自動回應====
                                Msg_ReAct(Talker, Msg);

                                //棒讀醬反饋訊息
                                if (Msg.Contains("やっほー") && Talker == "theforeverlly")  
                                {
                                    if (Talker != current_channel.ToLower())
                                    {
                                        Push_A_message_to_Room(current_channel + " : Ver 1.4b 內部測試 やっほー\n");
                                        irc.sendchatMessage("Ver 1.4b 內部測試 やっほー");
                                    }
                                }
                            }
                        }//End_ else Package.MsgIsCommand()
                        Package = null; //Let GC recycle this memory.
                    }//End_ while(OriMsg_object_queue.Count != 0)
                }//End_ interrupt
            }
        }

        //用thread的方式建立，並與Twitch伺服器做溝通.
        private void Enter_IRC_Room()       //IRC聊天室的主控制程序
        {
            string msg = null;

            Push_A_message_to_Room("***twitcco 內部測試版 START\n");
            Push_A_message_to_Room("***登入Twitch IRC\n");
            try
            {
                SpeechQueue_Push("twiticco", "MSG", true);
                if (TTS_T.IsAlive)
                {
                    TTS_T.Interrupt();
                }
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
                    Push_A_message_to_Room("Irc Error : " + ex.Message + "\n伺服器或網路影響?\n");
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
                        OriMsgQueue_Push(msg);  //將接收的訊息放入Queue, 交給執行緒Msg_Reader
                        msg_reader.Interrupt();
                    }
                }
            }
        }

        private void IRCRoom_AutoPing()
        {
            while (true)
            {
                Thread.Sleep(180000);
                irc.sendMessage("PING tmi.twitch.tv\r\n");
            }
        }



    }
}

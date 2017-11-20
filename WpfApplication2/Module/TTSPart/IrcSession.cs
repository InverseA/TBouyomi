using NAudio.Wave;
using System;
using System.Threading;
using System.Windows.Media;

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
                PutSystemMsg("登入失敗，帳號或密碼錯誤？\n", Brushes.LightGray);
            }
            else
            {
                IRCRoom = new Thread(Enter_IRC_Room);
                IRCRoom.IsBackground = true;    //如此使得main thread 結束後子thread 也可以跟著被關閉.
                IRCRoom.Start();
                AutoPing = new Thread(IRCRoom_AutoPing);
                AutoPing.IsBackground = true;   //如此使得main thread 結束後子thread 也可以跟著被關閉.
                AutoPing.Start();

                Title = AppName + " : " + current_channel;
                IS_ALREADY_LOGIN = true;
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
                        if(Package.GetTalker() != null)
                        {
                            Push_A_message_to_Room(Package.GetTalker(), Brushes.MediumPurple);
                            Push_A_message_to_Room(" : " + Package.GetMsg().Replace("\n", "") + "\n");
                        }

                        if (AllowMODSE && MODLetSEPass) //當MOD暫時的強制打開棒讀
                            Package.SetPremium(true);

                        if (COMMAND_SPEECH)                  //如果以指令"!TB"來說話之功能 啟用
                            Package.Speech_Msg_By_command();
                        
                        Package.Speech_Content_Proccess();  //處理多餘的符號
                        Talker = Package.GetTalker();
                        Msg = Package.GetMsg();

                        /*
                        if (Package.IsRepeat())
                        {
                            PutSystemMsg("大量重複字詞帳號 : " + Talker + "\n", Brushes.DodgerBlue);
                        }
                        */

                        if (Package.GetBitsTotal() != null) //提醒有人cheers
                        {
                            NoticeUsr();
                            PutSystemMsg("◭"+Talker +" Cheers： "+ Package.GetBitsTotal() + " Bits!\n", Brushes.Goldenrod);
                            irc.sendchatMessage("感謝信心加持(っ^ω^ｃ)");
                        }
                        else if (Package.IsSub())
                        {
                            NoticeUsr();
                            PutSystemMsg("☕ " + Package.GetSubMsg() + "\n", Brushes.Goldenrod);
                            if(Msg != " ")
                            {
                                PutSystemMsg("訂閱訊息 : "+ Msg + "\n", Brushes.Goldenrod);
                            }
                            irc.sendchatMessage("感謝訂閱(`・ω・´)b");
                        }

                        if (Package.MsgIsCommand() == true) //如果是指令
                            Msg_Command_Proc(Package);  //判別並處理命令
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
                                
                                Msg = Education_String_Replace(Msg , Package.IsPremium());
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
                                        Push_A_message_to_Room(current_channel + " : Ver 1.7 內部測試 やっほー\n");
                                        irc.sendchatMessage("Ver 1.7 內部測試 やっほー");
                                    }
                                }
                                SpeechQueue_Push("", "SentenceEnd", false); //End of sentence Flag
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
            PutSystemMsg("已登入Twitch IRC\n", Brushes.DodgerBlue);
            try
            {
                if (TTS_T.IsAlive)
                {
                    TTS_T.Interrupt();
                }
                PutSystemMsg("棒讀開始\n", Brushes.DodgerBlue);
            }
            catch (Exception ex)
            {
                PutSystemMsg("第1句話失敗 Error : " + ex.Message + "\n", Brushes.Red);
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
                    if (ChannelChange_Flag)
                    {
                        ChannelChange_Flag = false;
                        IS_ALREADY_LOGIN = false;
                        PutSystemMsg("Irc Connection : " + ex.Message + "\n更換頻道\n", Brushes.LightBlue);
                    }
                    else
                        PutSystemMsg("Irc Connection Error : " + ex.Message + "\n伺服器或網路影響?\n", Brushes.Red);
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
                    if (msg.StartsWith("@badges"))
                    {
                        try
                        {
                            OriMsgQueue_Push(msg);  //將接收的訊息放入Queue, 交給執行緒Msg_Reader
                            msg_reader.Interrupt();
                        }
                        catch (Exception ex)
                        {
                            PutSystemMsg("第1句話失敗 Error : " + ex.Message + "\n", Brushes.Red);
                        }
                    }

                    if (msg.StartsWith(":jtv") && msg.Contains("hosting you"))
                    {
                        string[] _temp;
                        _temp = msg.Split(':');
                        msg = _temp[2];
                        NoticeUsr();
                        PutSystemMsg("📺來自Twitch訊息：" + msg.Replace("\n", "") + "\n", Brushes.BlueViolet);
                    }

                    if (msg.StartsWith("@ban-reason"))
                    {
                        string[] _temp;
                        _temp = msg.Split(':');
                        msg = _temp[2];
                        NoticeUsr();
                        PutSystemMsg("⚠頻道的管理者已將 " + msg.Replace("\n","") + " BAN掉了\n", Brushes.Red);
                    }
                }
            }
        }

        private void IRCRoom_AutoPing()
        {
            while (true)
            {
                if (IS_ALREADY_LOGIN)
                {
                    irc.sendMessage("PING tmi.twitch.tv\r\n");
                    Thread.Sleep(180000);
                }
                else
                {
                    Thread.Sleep(30000);
                }
            }
        }

        private void NoticeUsr()    //發出叮聲
        {
            try
            {
                string _SEPath = "sound";
                WaveStream _MainOutputStream = null;
                WaveChannel32 _volumeStream = null;
                WaveOutEvent _player = null;
                if (_player != null)     //初始化_player
                {
                    _player.Stop();
                    _player.Dispose();
                    _player = null;
                    _volumeStream.Dispose();
                    _volumeStream = null;
                    _MainOutputStream.Dispose();
                    _MainOutputStream = null;
                }
                _MainOutputStream = new Mp3FileReader(_SEPath + "/" + "ding.mp3");
                _volumeStream = new WaveChannel32(_MainOutputStream);
                _player = new WaveOutEvent();
                _player.Init(_volumeStream);
                _player.Volume = SE_Volume;
                _player.Play();

                _player = null;
            }
            catch (Exception ex)
            {
                PutSystemMsg("SE Error:" + ex.Message + "\n", Brushes.Red);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void MainWindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShutDown SD = new ShutDown();
            SD.Owner = this;
            if (SD.ShowDialog() == true)
            {
                //若沒有嚴謹確認NULL值，程式執行或結束時未使用的thread等會出現錯誤。
                /*  msg_reader
                 *  Do_Not_Disturb
                 *  IRCRoom
                 *  AutoPing
                 *  GC_T
                 */
                 
                if (IRCRoom != null && IRCRoom.IsAlive)
                {
                    IRCRoom.Abort();
                    irc.close_irc();
                }
                if (Do_Not_Disturb != null && Do_Not_Disturb.IsAlive)
                {
                    Do_Not_Disturb.Abort();
                }
                if (AutoPing != null && AutoPing.IsAlive)
                {
                    AutoPing.Abort();
                }
                if (GC_T != null && GC_T.IsAlive)
                {
                    GC_T.Abort();
                }
                if (msg_reader != null && msg_reader.IsAlive)
                {
                    msg_reader.Abort();
                }
                if (TTS_T != null && TTS_T.IsAlive)
                {
                    TTS_T.Abort();
                }

                string path = "Setting";
                string prisetting = "prisetting.txt";
                string nickname = "nickname.txt";
                string education = "education.txt";
                string react = "ReAct.txt";
                string except = "Except.txt";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileStream Fstream = null;

                Fstream = new FileStream(path + "/" + prisetting, FileMode.Truncate);  //清空txt文件
                if (Fstream != null)
                    Fstream.Close();
                
                Fstream = new FileStream(path + "/" + prisetting, FileMode.OpenOrCreate);
                
                StreamWriter FileWrite = new StreamWriter(Fstream);

                //========關閉確認後將使用者偏好之參數寫進檔案===========
                /*prisetting檔案內容
                 * PRISETTING
                 * Volume
                 * Rate
                 * Word_Limit
                 * DoNotTalkFunction
                 * AutoLogin
                 * SE Volume
                 * SE Cancel
                */
                try
                {
                    //temp_writer = FileRead.ReadLine();

                    if (Fstream != null)   //如果個人設定檔案之內容已經存在
                    {
                        Fstream.Position = 0;

                        FileWrite.WriteLine("PRISETTING");
                        FileWrite.WriteLine(Speech_Volume);
                        FileWrite.WriteLine(Speech_Rate);
                        FileWrite.WriteLine(Speech_Word_Limit);

                        if (DoNotTalkFunction == true)  //DoNotTalkFunction
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        if (Auto_Login == true)         //Auto_Login
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        FileWrite.WriteLine(SE_Volume);

                        if(SE_Cancel_Func == true)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        FileWrite.WriteLine(Speech_Pitch);

                        FileWrite.Flush();

                        //FileWrite.Close();
                        Fstream.Close();    //關閉檔案串流.
                    }
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("Error:" + ex.Message + "\n");
                }
                //=======================================================


                //================= Nick Name ==========================
                Fstream = null;
                Fstream = new FileStream(path + "/" + nickname, FileMode.Truncate);  //清空txt文件
                if (Fstream != null)
                    Fstream.Close();
                Fstream = new FileStream(path + "/" + nickname, FileMode.OpenOrCreate);
                FileWrite = new StreamWriter(Fstream);
                //FileWrite.AutoFlush = true;
                try
                {
                    if (Fstream != null)
                    {
                        Fstream.Position = 0;

                        FileWrite.WriteLine("NICKNAME");
                        if (NickNameList.Count > 0)
                        {
                            for (int i = 0; i < NickNameList.Count; i++)
                            {
                                FileWrite.WriteLine(NickNameList[i].GetTalker().Replace("\n", null));
                                FileWrite.WriteLine(NickNameList[i].Get_Talker_NK().Replace("\n", null));
                                FileWrite.Flush();
                            }
                        }
                    }
                    Fstream.Close();    //關閉檔案串流.
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("Error:" + ex.Message + "\n");
                }
                //=========================================================


                //================= Education File ========================
                Fstream = null;
                Fstream = new FileStream(path + "/" + education, FileMode.Truncate);  //清空txt文件
                if (Fstream != null)
                    Fstream.Close();
                Fstream = new FileStream(path + "/" + education, FileMode.OpenOrCreate);
                FileWrite = new StreamWriter(Fstream);
                //FileWrite.AutoFlush = true;
                try
                {
                    if (Fstream != null)
                    {
                        Fstream.Position = 0;

                        FileWrite.WriteLine("EDUCATION");
                        if (EducationList.Count > 0)
                        {
                            for (int i = 0; i < EducationList.Count; i++)
                            {
                                FileWrite.WriteLine(EducationList[i].GetWord().Replace("\n", null));
                                FileWrite.WriteLine(EducationList[i].Get_Word_ed().Replace("\n", null));
                                FileWrite.Flush();
                            }
                        }
                    }
                    Fstream.Close();    //關閉檔案串流.
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("Error:" + ex.Message + "\n");
                }

                //=========================================================


                //===================ReAct File============================
                Fstream = null;
                Fstream = new FileStream(path + "/" + react, FileMode.Truncate);  //清空txt文件
                if (Fstream != null)
                    Fstream.Close();
                Fstream = new FileStream(path + "/" + react, FileMode.OpenOrCreate);
                FileWrite = new StreamWriter(Fstream);
                //FileWrite.AutoFlush = true;
                try
                {
                    if (Fstream != null)
                    {
                        Fstream.Position = 0;

                        FileWrite.WriteLine("REACT");
                        if (ReActList.Count > 0)
                        {
                            for (int i = 0; i < ReActList.Count; i++)
                            {
                                FileWrite.WriteLine(ReActList[i].GetWord().Replace("\n", null));
                                FileWrite.WriteLine(ReActList[i].Get_word_ra().Replace("\n", null));
                                FileWrite.Flush();
                            }
                        }
                    }
                    Fstream.Close();    //關閉檔案串流.
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("Error:" + ex.Message + "\n");
                }

                //=========================================================


                //================= Except File ========================
                Fstream = null;
                Fstream = new FileStream(path + "/" + except, FileMode.Truncate);  //清空txt文件
                if (Fstream != null)
                    Fstream.Close();
                Fstream = new FileStream(path + "/" + except, FileMode.OpenOrCreate);
                FileWrite = new StreamWriter(Fstream);
                //FileWrite.AutoFlush = true;
                try
                {
                    if (Fstream != null)
                    {
                        Fstream.Position = 0;

                        FileWrite.WriteLine("EXCEPT");
                        if (ExceptList.Count > 0)
                        {
                            for (int i = 0; i < ExceptList.Count; i++)
                            {
                                FileWrite.WriteLine(ExceptList[i].GetName().Replace("\n", null));
                                FileWrite.Flush();
                            }
                        }
                    }
                    Fstream.Close();    //關閉檔案串流.
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("Error:" + ex.Message + "\n");
                }

                //=========================================================

                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}

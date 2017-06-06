using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void change_channel(object sender, RoutedEventArgs e)   //當Login視窗被呼叫.
        {
            string path = "Setting";
            string accfile = "accpass.txt";
            StreamWriter FileWrite;
            StreamReader FileRead;

            Login cc = new Login();
            cc.Owner = this;
            FileStream Fstream = null;
            Fstream = new FileStream(path + "/" + accfile, FileMode.OpenOrCreate);
            FileRead = new StreamReader(Fstream);
            if (Fstream != null)   //如果檔案已經存在
            {
                //tempreader = FileRead.ReadLine();
                if (FileRead.ReadLine() != null)
                {
                    Fstream.Position = 0;   //return to the Beginning of the FileStream.
                    FileRead.DiscardBufferedData(); //清空buffer的內容並確保是乾淨的
                    cc.Channel_Account.Clear();
                    cc.OAuth.Clear();
                    cc.Channel_Account.AppendText(FileRead.ReadLine());
                    cc.OAuth.AppendText(FileRead.ReadLine());
                }
                
            }
            Fstream.Close();
            
            if (cc.ShowDialog() == true)    //當按下確定按鈕.
            {
                Fstream = new FileStream(path + "/" + accfile, FileMode.Truncate);  //清空txt文件
                Fstream.Close();

                Fstream = new FileStream(path + "/" + accfile, FileMode.OpenOrCreate);
                FileWrite = new StreamWriter(Fstream);

                current_channel = cc.Channel_Account.Text;
                current_OAuth = cc.OAuth.Text;

                //儲存登入資訊
                Fstream.Position = 0;   //return to the Beginning of the FileStream.
                FileWrite.WriteLine(current_channel);
                FileWrite.WriteLine(current_OAuth);

                FileWrite.Close();
                Fstream.Close();    //關閉檔案串流.
                try
                {
                    if (IRCRoom != null && IRCRoom.IsAlive)
                    {
                        IRCRoom.Abort();
                        irc.close_irc();
                        Push_A_message_to_Room("\n***改變頻道聊天室至 " + current_channel + " \n");
                    }
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("Error:" + ex.Message + "\n");
                }
                StartSession(); //開始IRC的工作
            }

        }
    }
}

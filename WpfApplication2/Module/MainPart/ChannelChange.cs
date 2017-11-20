using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Twitch_Bouyomi.View;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void ChannelChange_Click(object sender, RoutedEventArgs e)   //當Login視窗被呼叫.
        {
            string path = "Setting";
            string ACCfile = "accpass.txt";
            StreamWriter FileWrite;
            StreamReader FileRead;
            Login cc = new Login();
            cc.Owner = this;

            FileStream Fstream = null;
            Fstream = new FileStream(path + "/" + ACCfile, FileMode.Open);
            FileRead = new StreamReader(Fstream);

            //讀取accpass.txt的內容至頁面
            string _temp = FileRead.ReadLine();
            if (!IsFileEmpty(_temp))
            {
                current_channel = FileRead.ReadLine();
                cc.OAuth_pswd.Password = FileRead.ReadLine();
            }
            Fstream.Close();

            //當login視窗返回true值
            if (cc.ShowDialog() == true)
            {
                Fstream = new FileStream(path + "/" + ACCfile, FileMode.Truncate);  //清空txt文件
                Fstream.Close();
                
                Fstream = new FileStream(path + "/" + ACCfile, FileMode.Open);
                FileWrite = new StreamWriter(Fstream);
                current_channel = cc.Channel_Account;
                current_OAuth = cc.OAuth_pswd.Password;

                //儲存登入資訊
                Fstream.Position = 0;
                FileWrite.WriteLine("FileIsEmpty = false");
                FileWrite.WriteLine(current_channel);
                FileWrite.WriteLine(current_OAuth);
                FileWrite.Close();
                Fstream.Close();
                
                try
                {
                    if (IRCRoom != null && IRCRoom.IsAlive)
                    {
                        ChannelChange_Flag = true;
                        IRCRoom.Abort();
                        irc.close_irc();
                        PutSystemMsg("\n***改變頻道聊天室至 " + current_channel + " \n", Brushes.LightGray);
                    }
                }
                catch (Exception ex)
                {
                    PutSystemMsg("Error:" + ex.Message + "\n", Brushes.Red);
                }
                StartSession(); //開始IRC的工作
            }
        }
    }
}

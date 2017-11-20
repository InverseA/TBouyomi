using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                WritePersonalSetting();
                WriteNickNameFile();
                WriteEducationFile();
                WriteReActFile();
                WriteExceptFile();
                
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}

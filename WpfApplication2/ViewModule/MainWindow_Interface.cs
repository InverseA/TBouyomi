using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void OpenLive2DWindow_Click(object sender, RoutedEventArgs e)   //開啟Live2D
        {
            if (Live2D_T == null)
            {
                Live2D_T = new Thread(OpenLive2DWindow);
                Live2D_T.IsBackground = true;
                Live2D_T.Start();
            }
            else
            {
                if (LIVE2D_CLIENT_EXIST)
                    PutSystemMsg("Live2D視窗正在執行\n", Brushes.Green);
                else
                {
                    Begin_Live2D_Client();
                }
            }
        }

        private void Open_AdSetWindow(object sender, RoutedEventArgs e) //開啟進階設定介面
        {
            AdvancedSetting st = new AdvancedSetting();
            st.Owner = this;
            st.ShowDialog();
        }

        private void keyin_command(object sender, KeyEventArgs e)       //發話框處理
        {
            if (e.Key == Key.Return)
            {
                if (!IS_ALREADY_LOGIN)
                {
                    PutSystemMsg("您尚未登入聊天室！\n", Brushes.Red);
                    enter_command.Clear();
                    return;
                }

                if (enter_command.Text != "")
                {
                    irc.sendchatMessage(enter_command.Text);
                    OriMsgQueue_Push("@MsgFromUSR,PRIVMSG@" + current_channel + ".tmi :" + enter_command.Text + "\n");
                    msg_reader.Interrupt();
                    enter_command.Clear();
                }
            }
        }

        private void keyin_Gotfocus(object sender, RoutedEventArgs e)       //游標點擊發話框
        {
            EnterCommand_Label.Content = "";
        }

        private void keyin_LostFocus(object sender, RoutedEventArgs e)      //游標點擊發話框外
        {
            if(enter_command.Text == "")
                EnterCommand_Label.Content = "輸入訊息";
        }

        private void Cheer_Mode_Click(object sender, RoutedEventArgs e)          //Cheer Mode 轉換
        {
            if (SPEAK_PAUSE == false)
            {
                SPEAK_PAUSE = true;
                Speak_Mana.Content = "小奇點模式";
                PutSystemMsg("===已進入Cheers模式===\n", Brushes.ForestGreen);
                Color temp = Color.FromArgb(255, 184, 110, 0);
                Speak_Mana.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 200, 0));
                TBwindow.NonActiveBorderBrush = new SolidColorBrush(temp);
                TBwindow.NonActiveGlowBrush = new SolidColorBrush(temp);
                TBwindow.BorderBrush = new SolidColorBrush(temp);
                TBwindow.GlowBrush = new SolidColorBrush(temp);
            }
            else
            {
                SPEAK_PAUSE = false;
                Speak_Mana.Content = "普通模式";
                PutSystemMsg("===已離開Cheers模式===\n", Brushes.ForestGreen);
                Color temp = Color.FromArgb(255, 0, 76, 153);
                Speak_Mana.Foreground = new SolidColorBrush(Color.FromArgb(255, 211, 211, 211));
                TBwindow.BorderBrush = new SolidColorBrush(temp);
                TBwindow.GlowBrush = new SolidColorBrush(temp);
                temp = Color.FromArgb(255, 102, 102, 102);
                TBwindow.NonActiveBorderBrush = new SolidColorBrush(temp);
                TBwindow.NonActiveGlowBrush = new SolidColorBrush(temp);
            }
        }




    }
}

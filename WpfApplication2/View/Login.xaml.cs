using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Navigation;
using System.IO;

namespace Twitch_Bouyomi.View
{
    /// <summary>
    /// Login.xaml 的互動邏輯
    /// </summary>
    /// 
    public partial class Login
    {
        private IrcClient irc;
        string msg;
        private string channel_Account = null;

        public Login()
        {
            InitializeComponent();
            if (MainWindow.IsAutoLogin())
                Auto_Login_check.IsChecked = true;
            else
                Auto_Login_check.IsChecked = false;
        }

        private void AccountSave(object sender, RoutedEventArgs e)
        {
            OAuth_ID_X.Content = "";
            if (OAuth_pswd.Password == "")
                OAuth_ID_X.Content = "X";
            else
            {
                irc = new IrcClient("irc.twitch.tv", 6667, "twitcco", OAuth_pswd.Password);
                msg = irc.readMessage();
                if (msg.Contains("Login authentication failed"))
                {
                    Login_Wrong.Content = "請檢查Oauth是否有誤。";
                    irc.close_irc();
                }
                else
                {
                    int _index = 0;
                    _index = msg.IndexOf(":tmi.twitch.tv");
                    _index += 19;
                    msg = msg.Substring(19);
                    _index = msg.IndexOf(":Welcome");
                    _index--;
                    msg = msg.Remove(_index);

                    channel_Account = msg;
                    Result();
                }
            }
        }

        private void MainWindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Result()
        {
            irc.close_irc();
            this.DialogResult = true;
        }

        private void GetOAuth(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void IRC_intro(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void HandleAutoLoginCheck(object sender, RoutedEventArgs e)
        {
            MainWindow.SetAutoLogin(true);
        }

        private void HandleAutoLoginUnchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.SetAutoLogin(false);
        }

        public string Channel_Account { get => channel_Account; set => channel_Account = value; }
    }
}

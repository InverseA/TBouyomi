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
using MahApps.Metro.Controls;

namespace Twitch_Bouyomi
{
    /// <summary>
    /// Login.xaml 的互動邏輯
    /// </summary>
    /// 
    public partial class Login : MetroWindow
    {
        private IrcClient irc;
        string msg;

        public Login()
        {
            InitializeComponent();
            if (MainWindow.IsAutoLogin())
                Auto_Login_check.IsChecked = true;
            else
                Auto_Login_check.IsChecked = false;
        }

        private void Channel_set_ok(object sender, RoutedEventArgs e)
        {
            Account_X.Content = "";
            OAuth_ID_X.Content = "";

            if (Channel_Account.Text == "")
            {
                Account_X.Content = "X";
            }
            else if (OAuth.Text == "")
            {
                OAuth_ID_X.Content = "X";
            }
            else
            {
                irc = new IrcClient("irc.twitch.tv", 6667, Channel_Account.Text.ToLower(), OAuth.Text);
                msg = irc.readMessage();
                if (msg.Contains("Login authentication failed"))
                {
                    Login_Wrong.Content = "登入失敗，請檢查帳號或Oauth是否有誤。";
                    irc.close_irc();
                }
                else
                    Result();
            }
        }

        private void Result()
        {
            irc.close_irc();
            this.DialogResult = true;
        }

        private void MainWindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = false;
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
    }
}

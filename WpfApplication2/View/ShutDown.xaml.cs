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
    public partial class ShutDown : MetroWindow
    {
        public ShutDown()
        {
            InitializeComponent();
        }
        
        private void ShutDown_Sure(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ShutDown_Cancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        
        private void MainWindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

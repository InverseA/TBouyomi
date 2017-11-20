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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Twitch_Bouyomi.View
{
    /// <summary>
    /// DeleteConfirm.xaml 的互動邏輯
    /// </summary>
    public partial class DeleteConfirm
    {
        public DeleteConfirm()
        {
            InitializeComponent();
        }

        private void DeleteSure(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void DeleteCancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

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
    /// Live2DControl.xaml 的互動邏輯
    /// </summary>
    public partial class Live2DControl
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;

        public Live2DControl()
        {
            InitializeComponent();
            Set_All_CharaName();

            AutoUseLive2D_AtStart.IsChecked = MainWindow.Get_AUTO_START_LIVE2D_value();
            if (!GetMicState())
            {
                AutoUseLive2D_AtStart.IsChecked = false;
            }

            Set_Live2D_SpeakMode();

        }

        private void Set_All_CharaName()
        {
            List<string> Live2Dfolder = MainWindow.GetAllCharaName();    //***DEBUG***

            foreach (string _name in Live2Dfolder)
            {
                Live2DFolderCombobox.Items.Add(_name);
            }
            Live2DFolderCombobox.SelectedItem = MainWindow.Get_Live2D_DefaultChara();
        }

        private void AutoUseLive2D_AtStart_Check(object sender, RoutedEventArgs e)
        {
            if (!GetMicState())
            {
                MicExist.Content = "麥克風未接上?";
                AutoUseLive2D_AtStart.IsChecked = false;
            }
            else
            {
                MicExist.Content = "";
                MainWindow.Set_Live2D_AutoStart(true);
            }
        }

        private void AutoUseLive2D_AtStart_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.Set_Live2D_AutoStart(false);
        }

        private void ChangeLive2DMode_1(object sender, RoutedEventArgs e)
        {
            window.Set_Live2D_SpeakMode("Usr");
        }

        private void ChangeLive2DMode_2(object sender, RoutedEventArgs e)
        {
            window.Set_Live2D_SpeakMode("TB");
        }

        private void Set_Live2D_SpeakMode()
        {
            int Live2D_SpeakMode = MainWindow.Get_LIVE2D_SPEAK_MODE_value();
            if (Live2D_SpeakMode == 0)
                ChangeLive2DMode.IsChecked = false;
            else
                ChangeLive2DMode.IsChecked = true;
        }

        private void Live2D_CharaChange(object sender, SelectionChangedEventArgs e)
        {
            bool result = MainWindow.CharaChange(Live2DFolderCombobox.SelectedValue.ToString());
            if (!result)
                Live2DChara_Exist.Content = "所需文件遺失";
            else
                Live2DChara_Exist.Content = "";
        }

        private bool GetMicState()
        {
            return window.GetMicExist();
        }


    }
}

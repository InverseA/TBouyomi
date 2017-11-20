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
    /// BouyomiSetting.xaml 的互動邏輯
    /// </summary>
    public partial class BouyomiSetting
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        public BouyomiSetting()
        {
            InitializeComponent();

            WPF_Volume_slider.Value = MainWindow.Get_Bouyomi_Volume();
            WPF_Rate_slider.Value = MainWindow.Get_Speech_Rate();
            Pitch_num_slider.Value = MainWindow.Get_Speech_Pitch();

            WordNum_Limit_slider.Value = MainWindow.Get_Speech_Word_Limit();
            
            Speech_By_Command.IsChecked = MainWindow.Get_IsCommandSpeech();

            SE_Volume_slider.Value = window.Get_SE_Volume();
            SE_Pause.IsChecked = window.Get_SEPause();

            DefaultLanguageCB.Items.Add("正體中文(TW)");
            DefaultLanguageCB.Items.Add("正體中文(HK)");
            string _Ltemp = MainWindow.Get_DefaultLanguage();
            switch (_Ltemp)
            {
                case "TW":
                    DefaultLanguageCB.SelectedItem = "正體中文(TW)";
                    break;
                case "HK":
                    DefaultLanguageCB.SelectedItem = "正體中文(HK)";
                    break;
                default:
                    DefaultLanguageCB.SelectedItem = "正體中文(TW)";
                    break;
            }

        }

        //========== Bouyomi ===========
        
        private void WPF_DragLeave_Volume_Slider(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Set_Bouyomi_Volume(WPF_Volume_slider_box.Text);
        }
        
        private void WPF_DragLeave_Rate_Slider(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Set_Speech_Rate(WPF_Rate_slider_box.Text);
        }

        private void Pitch_ValueChanged(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Pitch_ValueChanged(Pitch_num_box.Text);
        }

        private void WPF_DragLeave_NumLimit_Slider(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SpeechWordNum_Limit(WordNum_Limitslider_box.Text);
        }
        
        private void Command_Speech_Check(object sender, RoutedEventArgs e)
        {
            MainWindow.Command_Speech_Switch(true);
        }

        private void Command_Speech_unCheck(object sender, RoutedEventArgs e)
        {
            MainWindow.Command_Speech_Switch(false);
        }

        //========== SE ==========
        private void SE_Volume_slider_ValueChanged(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SE_Volume_slider_ValueChanged(SE_Volume_box.Text);
        }

        private void SE_Pause_Check(object sender, RoutedEventArgs e)
        {
            window.SE_Pause_Switch(true);
        }

        private void SE_Pause_unCheck(object sender, RoutedEventArgs e)
        {
            window.SE_Pause_Switch(false);
        }

        private void DefaultLanguageChange(object sender, SelectionChangedEventArgs e)
        {
            switch (DefaultLanguageCB.SelectedValue.ToString())
            {
                case "正體中文(TW)":
                    MainWindow.Set_DefaultLanguage("TW");
                    break;
                case "正體中文(HK)":
                    MainWindow.Set_DefaultLanguage("HK");
                    break;
                default:
                    MainWindow.Set_DefaultLanguage("TW");
                    break;
            }
        }
    }
}

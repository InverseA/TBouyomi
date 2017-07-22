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
    /// AdvancedSetting.xaml 的互動邏輯
    /// </summary>
    public partial class AdvancedSetting : MetroWindow
    {
        public AdvancedSetting()
        {
            InitializeComponent();

            Pitch_num_slider.Value = MainWindow.Get_Speech_Pitch();
            SE_Volume_slider.Value = MainWindow.Get_SE_Volume();
            WordNum_Limit_slider.Value = MainWindow.Get_Speech_Word_Limit();
            Btis_Limit_slider.Value = MainWindow.Get_Btis_Limit();

            SE_Cancel.IsChecked = MainWindow.Get_SE_CANCEL_FUNC();
            Speech_By_Command.IsChecked = MainWindow.Get_IsCommandSpeech();
            SE_Pause.IsChecked = MainWindow.Get_SPEAK_PAUSE();

            DoNotDisturb_checkBox.IsChecked = MainWindow.Get_IsDoNotDisturb();
        }

        private void Pitch_ValueChanged(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Pitch_ValueChanged(Pitch_num_box.Text);
        }

        private void SE_Volume_slider_ValueChanged(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SE_Volume_slider_ValueChanged(SE_Volume_box.Text);
        }

        private void WPF_DragLeave_NumLimit_Slider(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SpeechWordNum_Limit(WordNum_Limitslider_box.Text);
        }

        private void Btis_Limit_slider_change(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MinBitsSet(Btis_Limit_box.Text);
        }

        private void SE_Cancel_Check(object sender, RoutedEventArgs e)
        {
            MainWindow.SE_Cancel_Switch(true);
        }

        private void SE_Cancel_unCheck(object sender, RoutedEventArgs e)
        {
            MainWindow.SE_Cancel_Switch(false);
        }

        private void Command_Speech_Check(object sender, RoutedEventArgs e)
        {
            MainWindow.Command_Speech_Switch(true);
        }

        private void Command_Speech_unCheck(object sender, RoutedEventArgs e)
        {
            MainWindow.Command_Speech_Switch(false);
        }

        private void SE_Pause_Check(object sender, RoutedEventArgs e)
        {
            MainWindow.SE_Pause_Switch(true);
        }

        private void SE_Pause_unCheck(object sender, RoutedEventArgs e)
        {
            MainWindow.SE_Pause_Switch(false);
        }

        private void HandleDoNotTalkCheck(object sender, RoutedEventArgs e)
        {
            MainWindow.HandleDoNotTalk_Switch(true);
        }

        private void HandleDoNotTalkUnchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.HandleDoNotTalk_Switch(false);
        }

    }
}

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
    /// Experiment.xaml 的互動邏輯
    /// </summary>
    public partial class Experiment
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;

        public Experiment()
        {
            InitializeComponent();

            DoNotDisturb_checkBox.IsChecked = window.Get_IsDoNotDisturb();
            AutoSlowMode.IsChecked = window.Get_AutoSlowMode();
            ForceSlowMode.IsChecked = window.Get_ForceSlowMode();
            SECancel.IsChecked = window.Get_SECancel();
            SEOnly.IsChecked = window.Get_SEOnlyMode();
            MODSE.IsChecked = window.Get_AllowMODSE();

        }

        private void HandleDoNotTalkCheck(object sender, RoutedEventArgs e)
        {
            window.HandleDoNotTalk_Switch(true);
        }

        private void HandleDoNotTalkUnchecked(object sender, RoutedEventArgs e)
        {
            window.HandleDoNotTalk_Switch(false);
        }

        private void AutoSlowModeCheck(object sender, RoutedEventArgs e)
        {
            window.HandleAutoSlowMode_Switch(true);
            window.WakeUpAutoSlowModeController();
        }

        private void AutoSlowModeUnchecked(object sender, RoutedEventArgs e)
        {
            window.HandleAutoSlowMode_Switch(false);
            window.WakeUpAutoSlowModeController();
        }

        private void ForceSlowModeCheck(object sender, RoutedEventArgs e)
        {
            window.HandleForceSlowMode_Switch(true);
            window.WakeUpForceSlowModeController();
        }

        private void ForceSlowModeUnchecked(object sender, RoutedEventArgs e)
        {
            window.HandleForceSlowMode_Switch(false);
            window.WakeUpForceSlowModeController();
        }

        private void SECancelCheck(object sender, RoutedEventArgs e)
        {
            window.HandleSECancel_Switch(true);
        }

        private void SECancelUnchecked(object sender, RoutedEventArgs e)
        {
            window.HandleSECancel_Switch(false);
        }

        private void SEOnlyCheck(object sender, RoutedEventArgs e)
        {
            window.HandleSEOnlyMode_Switch(true);
        }

        private void SEOnlyUnchecked(object sender, RoutedEventArgs e)
        {
            window.HandleSEOnlyMode_Switch(false);
        }

        private void AllowMODSECheck(object sender, RoutedEventArgs e)
        {
            window.HandleAllowMODSE_Switch(true);
        }

        private void AllowMODSEUnchecked(object sender, RoutedEventArgs e)
        {
            window.HandleAllowMODSE_Switch(false);
        }
    }
}

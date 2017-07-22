using NAudio.CoreAudioApi;
using System;
using System.Threading;
using System.Windows.Threading;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.033) };
        //DispatcherTimer Disturb_State_timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(6) };
        //========================DoNotTalk Function===============================

        private void DoNotDisturb_TimerTick()
        {
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void DoNotDisturb()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(3600000);
                }
                catch (ThreadInterruptedException)
                {
                    DISTURB_STATE = true;
                    Thread.Sleep(1750);
                    DISTURB_STATE = false;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Device_comboBox.SelectedItem != null)
            {
                var device = (MMDevice)Device_comboBox.SelectedItem;
                if (device != null)
                {
                    int Volume = 0;
                    try
                    {
                        Volume = (int)(Math.Round(device.AudioMeterInformation.MasterPeakValue * 100));
                    }
                    catch (Exception)
                    {

                    }
                    //Volume = temp;
                    Mic_volume.Value = Volume;
                    if (DoNotTalkFunction == true)  //使用者是否啟用勿打擾功能
                    {
                        try
                        {
                            if (Volume >= DoNotDisturbThreshold + 4)
                            {
                                Do_Not_Disturb.Interrupt();
                            }
                            else if (Volume >= DoNotDisturbThreshold)
                            {
                                lock (thislock)
                                {
                                    DoNotDisturbThreshold += 1;
                                    if (DoNotDisturbThreshold > 25)
                                        DoNotDisturbThreshold = 25;
                                }
                            }
                            else
                            {
                                lock (thislock)
                                {
                                    DoNotDisturbThreshold -= 2;
                                    if (DoNotDisturbThreshold < 12)
                                        DoNotDisturbThreshold = 12;
                                }
                            }
                        }
                        catch(Exception)
                        {

                        }
                        
                    }
                }
            }
        }
    }
}
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Linq;
using System.Threading;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void DoNotDisturbInit()
        {
            //防搶話機能Thread
            try
            {
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
                var deviceslist = devices.ToArray();
                int arraynum = deviceslist.Length;
                for (int i = 0; i < arraynum; i++)
                {
                    Device_comboBox.Items.Add(deviceslist[i]);
                }
                Device_comboBox.SelectedItem = deviceslist[0];
                if (arraynum != -1)
                {
                    if (Do_Not_Disturb == null)
                    {
                        Do_Not_Disturb = new Thread(DoNotDisturb);
                        Do_Not_Disturb.IsBackground = true;
                        Do_Not_Disturb.Start();

                        DoNotDisturb_TimerTick();

                        Int_WaveIn = new WaveIn();
                        Int_WaveIn.WaveFormat = new WaveFormat(44100, 1);
                        Int_WaveIn.StartRecording();
                        Mic_exist = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Push_A_message_to_Room("Error:" + ex.Message + "錄音裝置錯誤?\n");
            }
        }
    }
}

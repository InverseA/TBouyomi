using NAudio.Wave;
using System;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        WaveIn Int_WaveIn = null;
        WaveStream MainOutputStream;
        WaveChannel32 volumeStream;
        WaveOutEvent player;
        private double PlaySE(string SE)
        {
            string _SEPath = "SE";
            try
            {
                int index = SE_index(SE);
                if (player != null)     //初始化player
                {
                    player.Stop();
                    player.Dispose();
                    player = null;
                    volumeStream.Dispose();
                    volumeStream = null;
                    MainOutputStream.Dispose();
                    MainOutputStream = null;
                }

                MainOutputStream = new Mp3FileReader(_SEPath + "/" + SEList[index].GetSEFileName());
                volumeStream = new WaveChannel32(MainOutputStream);

                player = new WaveOutEvent();
                player.Init(volumeStream);
                player.Volume = SE_Volume;
                player.Play();
                string[] _SETotalTime_S = volumeStream.TotalTime.ToString().Split(':');
                double _SETotalTime = 0;
                _SETotalTime += Double.Parse(_SETotalTime_S[0]) * 3600;
                _SETotalTime += Double.Parse(_SETotalTime_S[1]) * 60;
                _SETotalTime += Double.Parse(_SETotalTime_S[2]);

                return _SETotalTime;
            }
            catch (Exception ex)
            {
                PutSystemMsg("SE Error:" + ex.Message + "\n", Brushes.Red);

                return -1;
            }
        }
    }
}

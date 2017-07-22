using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void GC_Thread()
        {
            while (true)
            {
                Thread.Sleep(300000);
                if (Mic_exist)
                {
                    try
                    {
                        Int_WaveIn.StopRecording();
                        Int_WaveIn.StartRecording();
                    }
                    catch(Exception)
                    {

                    }
                }
            }
        }
    }
}

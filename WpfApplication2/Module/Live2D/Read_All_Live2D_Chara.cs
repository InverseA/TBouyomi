using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Read_All_Live2D_Chara()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(@"Live2D\Assets");
                foreach (string dir in dirs)
                {
                    int _index = 0;
                    string _temp = null;
                    _index = dir.LastIndexOf("\\");
                    _temp = dir.Remove(0, _index + 1);
                    Live2Dfolder.Add(_temp);
                }
            }
            catch(Exception ex)
            {
                PutSystemMsg(ex+"\n",Brushes.Red);
            }
        }
    }
}

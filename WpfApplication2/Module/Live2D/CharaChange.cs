using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        public static bool CharaChange(string _CharaName)
        {
            if (!CheckFile(_CharaName))
            {
                return false;
            }
            else
            {
                LIVE2D_USR_DEFAULT_CHARA = _CharaName;
                if (Live2D_T != null)
                {
                    CHANGE_CHARA_NAME = _CharaName;
                    lock (Live2D_Event_lock)
                    {
                        LIVE2D_INTERPT_EVENT = 1;  //change Live2D character event.
                    }
                }
                return true;
            }
        }

        private static bool CheckFile(string _Name) //check file exist or not.
        {
            string folder = "Live2D/Assets/" + _Name + "/";
            if (!File.Exists(folder + _Name + ".moc3"))
                return false;
            if (!File.Exists(folder + _Name + ".model3.json"))
                return false;
            if (!File.Exists(folder + _Name + ".png"))
                return false;
            if (!File.Exists(folder + "Idle.motion3.json"))
                return false;
            if (!File.Exists(folder + "Talk.motion3.json"))
                return false;

            return true;
        }
    }
    

}

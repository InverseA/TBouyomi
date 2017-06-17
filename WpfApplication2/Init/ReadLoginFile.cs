using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void ReadLoginFile()
        {
            string path = "Setting";
            string accfile = "accpass.txt";
            
            FileStream Fstream = null;
            Fstream = new FileStream(path + "/" + accfile, FileMode.OpenOrCreate);
            StreamReader FileRead = null;
            FileRead = new StreamReader(Fstream);
            if (Fstream != null)   //如果檔案已經存在
            {
                //tempreader = FileRead.ReadLine();
                if (FileRead.ReadLine() != null)
                {
                    Fstream.Position = 0;   //return to the Beginning of the FileStream.
                    FileRead.DiscardBufferedData(); //清空buffer的內容並確保是乾淨的
                    current_channel = FileRead.ReadLine();
                    current_OAuth = FileRead.ReadLine();
                }
            }
            Fstream.Close();
        }
    }
}

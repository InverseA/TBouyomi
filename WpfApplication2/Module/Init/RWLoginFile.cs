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
            string ACCFile = "accpass.txt";
            bool _FileNotExist = false;
            
            FileStream Fstream = null;

            try
            {
                Fstream = new FileStream(path + "/" + ACCFile, FileMode.Open);
            }
            catch (Exception)
            {
                _FileNotExist = true;
                Fstream = new FileStream(path + "/" + ACCFile, FileMode.OpenOrCreate);  //如果檔案不存在(第一次使用)
            }

            if (!_FileNotExist) //如果存在檔案(非第一次使用)
            {
                StreamReader FileRead = null;
                FileRead = new StreamReader(Fstream);
                Fstream.Position = 0;
                FileRead.DiscardBufferedData(); //清空buffer的內容並確保是乾淨的

                //取得AccFileIsEmpty的字串值
                string _temp = FileRead.ReadLine();
                if (!IsFileEmpty(_temp))
                {
                    current_channel = FileRead.ReadLine().ToLower();
                    current_OAuth = FileRead.ReadLine();
                }
            }
            else
            {
                StreamWriter FileWrite = null;
                FileWrite = new StreamWriter(Fstream);
                string _temp = "FileIsEmpty = true";
                Fstream.Position = 0;
                FileWrite.WriteLine(_temp);
                FileWrite.Close();
            }
            Fstream.Close();
        }

        private bool IsFileEmpty(string _s)  //檢查accpass.txt中的AccFileIsEmpty旗標
        {
            if (_s == null)
                return true;
            else if (!_s.Contains("FileIsEmpty"))
                return true;
            else
            {
                int _index = 0;
                _index = _s.IndexOf("=");
                _index += 2;
                _s = _s.Remove(0, _index);
                if (_s == "true")
                    return true;
                else
                    return false;
            }
        }
    }
}

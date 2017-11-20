using System;
using System.IO;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void GetSEFileList()
        {
            string SEPath = "SE";
            string[] SeFiles = Directory.GetFiles(SEPath, "*.mp3");

            for(int i = 0; i < SeFiles.Length; i++)
            {
                SeFiles[i] = Path.GetFileName(SeFiles[i]);
            }
            using (FileStream Fstream = new FileStream(SEPath + "/" + "SEList.txt", FileMode.Create))
            {
                using (StreamWriter FileWrite = new StreamWriter(Fstream))
                {
                    try
                    {
                        if (Fstream != null)
                        {
                            string temp;
                            Fstream.Position = 0;
                            for (int i = 0; i < SeFiles.Length; i++)
                            {
                                Add_SE(SeFiles[i].ToString());
                                temp = SeFiles[i].ToString().Remove(SeFiles[i].ToString().LastIndexOf("."));
                                FileWrite.WriteLine(temp);
                                FileWrite.Flush();
                            }
                        }
                        SEList.Reverse();
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("Error 讀取SE檔案:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }
    }
}

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
        private void ReadExceptFile()
        {
            string path = "Setting";
            string education = "except.txt";
            string temp_reader1 = null;
            using (FileStream Fstream = new FileStream(path + "/" + education, FileMode.OpenOrCreate))
            {
                using (StreamReader FileRead = new StreamReader(Fstream))
                {
                    try     //========== Read file nickname ==========
                    {
                        temp_reader1 = FileRead.ReadLine();
                        if (temp_reader1 == "EXCEPT")   //如果個人設定檔案之內容已經存在
                        {
                            do
                            {
                                temp_reader1 = FileRead.ReadLine();
                                if (temp_reader1 == null)
                                    break;
                                Add_Except(temp_reader1);
                            } while (true);
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("ReadExceptFile Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }

        public void WriteExceptFile()
        {
            string path = "Setting";
            string except = "Except.txt";
            using (FileStream Fstream = new FileStream(path + "/" + except, FileMode.Create))
            {
                using (StreamWriter FileWrite = new StreamWriter(Fstream))
                {
                    try
                    {
                        if (Fstream != null)
                        {
                            FileWrite.WriteLine("EXCEPT");
                            if (ExceptList.Count > 0)
                            {
                                for (int i = 0; i < ExceptList.Count; i++)
                                {
                                    FileWrite.WriteLine(ExceptList[i].GetName().Replace("\n", null));
                                    FileWrite.Flush();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("WriteExceptFile Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }
    }
}

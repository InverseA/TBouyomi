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
        private void ReadEducationFile()
        {
            string path = "Setting";
            string education = "education.txt";
            string temp_reader1 = null;
            string temp_reader2 = null;
            using (FileStream Fstream = new FileStream(path + "/" + education, FileMode.OpenOrCreate))
            {
                using (StreamReader FileRead = new StreamReader(Fstream))
                {
                    try     //========== Read file nickname ==========
                    {
                        temp_reader1 = FileRead.ReadLine();

                        /* nickname檔案順序
                         * edu
                         * change
                        */
                        if (temp_reader1 == "EDUCATION")   //如果個人設定檔案之內容已經存在
                        {
                            //Fstream.Position = 0;
                            do
                            {
                                temp_reader1 = FileRead.ReadLine();
                                if (temp_reader1 == null)
                                    break;
                                temp_reader2 = FileRead.ReadLine();
                                Add_Education(temp_reader1, temp_reader2);
                            } while (true);
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }

        public void WriteEducationFile()
        {
            string path = "Setting";
            string education = "education.txt";
            using (FileStream Fstream = new FileStream(path + "/" + education, FileMode.Create))
            {
                using (StreamWriter FileWrite = new StreamWriter(Fstream))
                {
                    try
                    {
                        FileWrite.WriteLine("EDUCATION");
                        if (EducationList.Count > 0)
                        {
                            for (int i = 0; i < EducationList.Count; i++)
                            {
                                FileWrite.WriteLine(EducationList[i].GetWord().Replace("\n", null));
                                FileWrite.WriteLine(EducationList[i].Get_Word_ed().Replace("\n", null));
                                FileWrite.Flush();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("WriteEducationFile Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }
    }
}

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
        private void ReadReActFile()
        {
            string path = "Setting";
            string ReAct = "ReAct.txt";
            string temp_reader1 = null;
            string temp_reader2 = null;
            using (FileStream Fstream = new FileStream(path + "/" + ReAct, FileMode.OpenOrCreate))
            {
                using (StreamReader FileRead = new StreamReader(Fstream))
                {
                    try     //========== Read file nickname ==========
                    {
                        FileRead.DiscardBufferedData();
                        temp_reader1 = FileRead.ReadLine();

                        /* nickname檔案順序
                         * word
                         * react
                        */
                        if (temp_reader1 == "REACT")   //如果個人設定檔案之內容已經存在
                        {
                            do
                            {
                                temp_reader1 = FileRead.ReadLine();
                                if (temp_reader1 == null)
                                    break;
                                temp_reader2 = FileRead.ReadLine();
                                Add_ReAct(temp_reader1, temp_reader2);
                            } while (true);
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("Error 讀取回應檔案:" + ex.Message + "\n", Brushes.Red);
                    }

                }
            }
        }

        public void WriteReActFile()
        {
            string path = "Setting";
            string react = "ReAct.txt";
            using (FileStream Fstream = new FileStream(path + "/" + react, FileMode.Create))
            {
                using (StreamWriter FileWrite = new StreamWriter(Fstream))
                {
                    try
                    {
                        FileWrite.WriteLine("REACT");
                        if (ReActList.Count > 0)
                        {
                            for (int i = 0; i < ReActList.Count; i++)
                            {
                                FileWrite.WriteLine(ReActList[i].GetWord().Replace("\n", null));
                                FileWrite.WriteLine(ReActList[i].Get_word_ra().Replace("\n", null));
                                FileWrite.Flush();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("WriteReActFile Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }
    }
}

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
        private void ReadNickNameFile()
        {
            string path = "Setting";
            string nickname = "nickname.txt";
            string temp_reader1 = null;
            string temp_reader2 = null;
            using (FileStream Fstream = new FileStream(path + "/" + nickname, FileMode.OpenOrCreate))
            {
                using (StreamReader FileRead = new StreamReader(Fstream))
                {
                    try     //========== Read file nickname ==========
                    {
                        temp_reader1 = FileRead.ReadLine();

                        /* nickname檔案順序
                         * Talker
                         * Talker_NickName
                        */
                        if (temp_reader1 == "NICKNAME")   //如果個人設定檔案之內容已經存在
                        {
                            do
                            {
                                temp_reader1 = FileRead.ReadLine();
                                if (temp_reader1 == null)
                                    break;
                                temp_reader2 = FileRead.ReadLine();
                                Add_NickName(temp_reader1, temp_reader2);
                            } while (true);
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("Error 讀取暱稱擋:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }

        private void WriteNickNameFile()
        {
            string path = "Setting";
            string nickname = "nickname.txt";
            using (FileStream Fstream = new FileStream(path + "/" + nickname, FileMode.Create))
            {
                using (StreamWriter FileWrite = new StreamWriter(Fstream))
                {
                    try
                    {
                        FileWrite.WriteLine("NICKNAME");
                        if (NickNameList.Count > 0)
                        {
                            for (int i = 0; i < NickNameList.Count; i++)
                            {
                                FileWrite.WriteLine(NickNameList[i].GetTalker().Replace("\n", null));
                                FileWrite.WriteLine(NickNameList[i].Get_Talker_NK().Replace("\n", null));
                                FileWrite.Flush();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("WriteNickNameFile Error:" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }
    }
}

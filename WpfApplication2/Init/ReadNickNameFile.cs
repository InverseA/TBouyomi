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
        private void ReadNickNameFile()
        {
            string path = "Setting";
            string nickname = "nickname.txt";
            string temp_reader1 = null;
            string temp_reader2 = null;
            FileStream Fstream = null;
            Fstream = new FileStream(path + "/" + nickname, FileMode.OpenOrCreate);
            StreamReader FileRead = new StreamReader(Fstream);
            
            try     //========== Read file nickname ==========
            {
                FileRead.DiscardBufferedData();
                temp_reader1 = FileRead.ReadLine();

                /* nickname檔案順序
                 * Talker
                 * Talker_NickName
                 * Talker
                 * Talker_NickName
                */
                if (temp_reader1 == "NICKNAME")   //如果個人設定檔案之內容已經存在
                {
                    //Fstream.Position = 0;
                    do
                    {
                        temp_reader1 = FileRead.ReadLine();
                        if (temp_reader1 == null)
                            break;
                        temp_reader2 = FileRead.ReadLine();
                        Add_NickName(temp_reader1, temp_reader2);
                    } while (true);

                    FileRead.Close();
                    Fstream.Close();    //關閉檔案串流.

                }
            }
            catch (Exception ex)
            {
                Push_A_message_to_Room("Error:" + ex.Message + "\n");
            }

            FileRead.Dispose(); //還回memory
            if (Fstream != null)
                Fstream.Dispose();
        }
    }

}

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
        private void ReadPersonalSetting()
        {
            string path = "Setting";
            string prisetting = "prisetting.txt";
            string temp_reader1 = null;

            FileStream Fstream = null;
            Fstream = new FileStream(path + "/" + prisetting, FileMode.OpenOrCreate);
            StreamReader FileRead = new StreamReader(Fstream);

            try     //========== Read file prisetting ==========
            {
                temp_reader1 = FileRead.ReadLine();

                /*prisetting檔案順序
                 * PRISETTING
                 * Volume
                 * Rate
                 * WordNumRestrict
                 * DoNotTalkFunction 防搶話
                 * AUTO_LOGIN
                 * SE Volume
                 * SE Cancel
                 * Speech_Pitch 音高
                 * COMMAND_SPEECH !TB指令棒讀
                 * Min_Btis_Limit 最小bits棒讀之限制
                */
                if (temp_reader1 == "PRISETTING")   //如果個人設定檔案之內容已經存在
                {
                    //Fstream.Position = 0;

                    Speech_Volume = Int32.Parse(FileRead.ReadLine());
                    WPF_Volume_slider.Value = Speech_Volume;
                    Speech_Rate = Int32.Parse(FileRead.ReadLine());
                    WPF_Rate_slider.Value = Speech_Rate;
                    Speech_Word_Limit = Int32.Parse(FileRead.ReadLine());

                    if (FileRead.ReadLine() == "1")  //DoNotTalkFunction
                    {
                        DoNotTalkFunction = true;
                    }
                    else
                    {
                        DoNotTalkFunction = false;
                    }

                    if (FileRead.ReadLine() == "1") //AutoLogin
                        AUTO_LOGIN = true;
                    else
                        AUTO_LOGIN = false;

                    temp_reader1 = FileRead.ReadLine();
                    if (!(temp_reader1 == null))
                    {
                        SE_Volume = float.Parse(temp_reader1);
                    }

                    temp_reader1 = FileRead.ReadLine();
                    if(temp_reader1 == "1")
                    {
                        SE_CANCEL_FUNC = true;
                    }
                    else
                    {
                        SE_CANCEL_FUNC = false;
                    }

                    Speech_Pitch = Int32.Parse(FileRead.ReadLine());

                    temp_reader1 = FileRead.ReadLine();
                    if (temp_reader1 == "1")
                    {
                        COMMAND_SPEECH = true;
                    }
                    else
                    {
                        COMMAND_SPEECH = false;
                    }

                    Min_Btis_Limit = Int32.Parse(FileRead.ReadLine());

                    FileRead.Close();
                    Fstream.Close();
                }

                FileRead.Dispose(); //還回memory
                if (Fstream != null)
                    Fstream.Dispose();
            }
            catch (Exception ex)
            {
                PutSystemMsg("Error 讀取設定檔:" + ex.Message + "\n", Brushes.Red);
            }

        }
    }
}

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
                 * DoNotTalkFunction
                 * AutoLogin
                 * SE Volume
                 * SE Cancel
                 * Pitch
                */
                if (temp_reader1 == "PRISETTING")   //如果個人設定檔案之內容已經存在
                {
                    //Fstream.Position = 0;

                    Speech_Volume = Int32.Parse(FileRead.ReadLine());
                    WPF_Volume_slider.Value = Speech_Volume;
                    Speech_Rate = Int32.Parse(FileRead.ReadLine());
                    WPF_Rate_slider.Value = Speech_Rate;
                    Speech_Word_Limit = Int32.Parse(FileRead.ReadLine());
                    WordNum_Limit_slider.Value = Speech_Word_Limit;

                    if (FileRead.ReadLine() == "1")  //DoNotTalkFunction
                    {
                        checkBox.IsChecked = true;
                        DoNotTalkFunction = true;
                    }
                    else
                    {
                        checkBox.IsChecked = false;
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
                        SE_Volume_slider.Value = (int)(SE_Volume * 100);
                    }

                    temp_reader1 = FileRead.ReadLine();
                    if(temp_reader1 == "1")
                    {
                        SE_Cancel.IsChecked = true;
                        SE_CANCEL_FUNC = true;
                    }
                    else
                    {
                        SE_Cancel.IsChecked = false;
                        SE_CANCEL_FUNC = false;
                    }

                    Speech_Pitch = Int32.Parse(FileRead.ReadLine());
                    Pitch_num_slider.Value = Speech_Pitch;

                    FileRead.Close();
                    Fstream.Close();
                }

                FileRead.Dispose(); //還回memory
                if (Fstream != null)
                    Fstream.Dispose();
            }
            catch (Exception ex)
            {
                Push_A_message_to_Room("Error:" + ex.Message + "\n");
            }

        }
    }
}

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
            using (FileStream Fstream = new FileStream(path + "/" + prisetting, FileMode.OpenOrCreate))
            {
                using (StreamReader FileRead = new StreamReader(Fstream))
                {
                    try     //========== Read file prisetting ==========
                    {
                        string temp_reader1 = null;
                        temp_reader1 = FileRead.ReadLine();

                        /*prisetting檔案順序
                         * PRISETTING
                         * Volume       棒讀音量
                         * Rate         棒讀速度
                         * WordNumRestrict  字數限制
                         * DoNotTalkFunction 防搶話
                         * AUTO_LOGIN   自動登入
                         * SE Volume    音效音量
                         * SE Cancel    音效間相互取消
                         * Speech_Pitch 音高
                         * COMMAND_SPEECH 啟用!TB指令棒讀
                         * Min_Btis_Limit 最小bits棒讀之限制
                         * SE Disable   停用音效
                         * AUTO_START_LIVE2D    自動啟用Live2D
                         * LIVE2D_SPEAK_MODE    Live2D說話模式
                         * LIVE2D_USR_DEFAULT_CHARA Live2D預設角色
                         * Default_Language 預設語音
                         * ForceSlowMode    強制慢動作模式
                         * AutoSlowMode     自動慢動作
                         * SEOnlyMode       音效模式
                         * AllowMODSE       允許MOD使用指令暫時開啟音效
                        */
                        if (temp_reader1 == "PRISETTING")   //如果個人設定檔案之內容已經存在
                        {
                            Speech_Volume = Int32.Parse(FileRead.ReadLine());
                            Speech_Rate = Int32.Parse(FileRead.ReadLine());
                            Speech_Word_Limit = Int32.Parse(FileRead.ReadLine());

                            //DoNotTalkFunction
                            if (FileRead.ReadLine() == "1")
                                DoNotTalkFunction = true;

                            //AutoLogin
                            if (FileRead.ReadLine() == "1")
                                AUTO_LOGIN = true;
                            else
                                AUTO_LOGIN = false;

                            //SE_Volume
                            temp_reader1 = FileRead.ReadLine();
                            if (!(temp_reader1 == null))
                                SE_Volume = float.Parse(temp_reader1);

                            //SE_CANCEL_FUNC 音效相互取消
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                SE_CANCEL_FUNC = true;

                            //Speech_Pitch 音高
                            Speech_Pitch = Int32.Parse(FileRead.ReadLine());

                            //COMMAND_SPEECH 啟用!TB指令棒讀
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                COMMAND_SPEECH = true;

                            //Min_Btis_Limit 最小bits棒讀之限制
                            Min_Btis_Limit = Int32.Parse(FileRead.ReadLine());

                            //SE_PAUSE 停用音效
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                SE_PAUSE = true;

                            //AUTO_START_LIVE2D    自動啟用Live2D
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                AUTO_START_LIVE2D = true;

                            //LIVE2D_SPEAK_MODE    Live2D說話模式
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                LIVE2D_SPEAK_MODE = 1;

                            //LIVE2D_USR_DEFAULT_CHARA Live2D預設角色
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 != "")
                                LIVE2D_USR_DEFAULT_CHARA = temp_reader1;

                            //Default_Language 預設語音
                            Default_Language = FileRead.ReadLine();

                            //ForceSlowMode 強制慢動作模式
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                ForceSlowMode = true;

                            //AutoSlowMode     自動慢動作
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "0")
                                AutoSlowMode = false;

                            //SEOnlyMode       音效模式
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                SEOnlyMode = true;

                            //AllowMODSE       允許MOD使用指令暫時開啟音效
                            temp_reader1 = FileRead.ReadLine();
                            if (temp_reader1 == "1")
                                AllowMODSE = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("讀取設定檔訊息：" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }

        private void WritePersonalSetting()
        {
            string path = "Setting";
            string prisetting = "prisetting.txt";
            //using (FileStream Fstream = new FileStream(path + "/" + prisetting, FileMode.Truncate))    //清空txt文件
            using (FileStream Fstream = new FileStream(path + "/" + prisetting, FileMode.Create))
            {
                using (StreamWriter FileWrite = new StreamWriter(Fstream))
                {
                    /*prisetting檔案順序
                    * PRISETTING
                    * Volume       棒讀音量
                    * Rate         棒讀速度
                    * WordNumRestrict  字數限制
                    * DoNotTalkFunction 防搶話
                    * AUTO_LOGIN   自動登入
                    * SE Volume    音效音量
                    * SE Cancel    音效間相互取消
                    * Speech_Pitch 音高
                    * COMMAND_SPEECH 啟用!TB指令棒讀
                    * Min_Btis_Limit 最小bits棒讀之限制
                    * SE Disable   停用音效
                    * AUTO_START_LIVE2D    自動啟用Live2D
                    * LIVE2D_SPEAK_MODE    Live2D說話模式
                    * LIVE2D_USR_DEFAULT_CHARA Live2D預設角色
                    * Default_Language 預設語音
                    */
                    try
                    {
                        Fstream.Position = 0;

                        FileWrite.WriteLine("PRISETTING");
                        FileWrite.WriteLine(Speech_Volume);
                        FileWrite.WriteLine(Speech_Rate);
                        FileWrite.WriteLine(Speech_Word_Limit);

                        if (DoNotTalkFunction == true)  //DoNotTalkFunction
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        if (AUTO_LOGIN == true)         //Auto_Login
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        FileWrite.WriteLine(SE_Volume);

                        if (SE_CANCEL_FUNC == true)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        FileWrite.WriteLine(Speech_Pitch);

                        if (COMMAND_SPEECH == true)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        FileWrite.WriteLine(Min_Btis_Limit);

                        //SE Disable   停用音效
                        if (SE_PAUSE)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        //AUTO_START_LIVE2D    自動啟用Live2D
                        if (AUTO_START_LIVE2D)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        //LIVE2D_SPEAK_MODE    Live2D說話模式
                        if (LIVE2D_SPEAK_MODE == 1)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        //LIVE2D_USR_DEFAULT_CHARA Live2D預設角色
                        FileWrite.WriteLine(LIVE2D_USR_DEFAULT_CHARA);

                        //Default_Language 預設語音
                        FileWrite.WriteLine(Default_Language);

                        //ForceSlowMode    強制慢動作模式
                        if (ForceSlowMode)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        //AutoSlowMode     自動慢動作
                        if (AutoSlowMode)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        //SEOnlyMode       音效模式
                        if (SEOnlyMode)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");

                        //AllowMODSE       允許MOD使用指令暫時開啟音效
                        if (AllowMODSE)
                            FileWrite.WriteLine("1");
                        else
                            FileWrite.WriteLine("0");
                    }
                    catch (Exception ex)
                    {
                        PutSystemMsg("寫入個人設定檔：" + ex.Message + "\n", Brushes.Red);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private static Object Live2D_Event_lock = new object();
        private static int LIVE2D_INTERPT_EVENT = 0;
        private static string CHANGE_CHARA_NAME = null;

        //Live2D Localhost Server Thread
        private void OpenLive2DWindow() 
        {
            //==啟動exe==
            Begin_Live2D_Client();
            //===========
            //byte[] bytes = new Byte[1024];    //Receive buff
            byte[] msg = null;
            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 7667);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                // Start listening for connections.
                while (true)
                {
                    Random r = new System.Random(1000);
                    int _tempInt = 0;
                    int _tempVolume = 0;
                    Socket handler = listener.Accept();
                    LIVE2D_CLIENT_EXIST = true;
                    while (true)
                    {
                        try
                        {
                            // An incoming connection needs to be processed.
                            /*
                            while (true)
                            {
                                bytes = new byte[1024];
                                int bytesRec = handler.Receive(bytes);
                                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                Console.WriteLine(data.ToString());
                                if (data.IndexOf("<EOF>") > -1)
                                {
                                    break;
                                }
                            }

                            // Show the data on the console.
                            Console.WriteLine("Text received : {0}", data);
                            */
                            // Echo the data back to the client.

                            var socketError = new SocketError();

                            if (socketError != SocketError.Success)
                            {
                                break;
                            }
                            else
                            {
                                if (LIVE2D_INTERPT_EVENT == 0)
                                {
                                    if (LIVE2D_SPEAK_MODE == 1)  //如果是使用者對嘴模式
                                    {
                                        if (VOLUME >= 13)
                                        {
                                            _tempVolume = VOLUME - 12;
                                            _tempVolume = (int)Math.Sqrt((_tempVolume * 200) - (_tempVolume * _tempVolume));
                                        }
                                        else if(VOLUME < 13)
                                        {
                                            _tempVolume = 0;
                                        }
                                        msg = System.Text.Encoding.ASCII.GetBytes(_tempVolume.ToString());
                                    }
                                    else
                                    {
                                        if (TBouyomiIsSpeak)
                                        {
                                            _tempInt = r.Next(-10, 0);
                                            _tempInt += Speech_Volume;
                                            if (_tempInt < 0)
                                                _tempInt = 0;
                                        }
                                        else
                                            _tempInt = 0;

                                        msg = System.Text.Encoding.ASCII.GetBytes(_tempInt.ToString());
                                    }
                                }
                                else if (LIVE2D_INTERPT_EVENT == 1)
                                {
                                    msg = System.Text.Encoding.ASCII.GetBytes("CC:" + CHANGE_CHARA_NAME + "<EOF>");
                                    lock (Live2D_Event_lock)
                                    {
                                        LIVE2D_INTERPT_EVENT = 0;
                                    }
                                }
                                //PutSystemMsg("Live2D Thread Send : " + Volume_String + "\n", Brushes.Blue);

                                handler.Send(msg);
                                Thread.Sleep(40);
                            }
                        }
                        catch (Exception ex)
                        {
                            PutSystemMsg("Live2D視窗關閉" + "\n", Brushes.DarkRed);
                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();

                            LIVE2D_CLIENT_EXIST = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PutSystemMsg("最外層例外 : " + "\n", Brushes.Red);
            }
        }

        private void Begin_Live2D_Client()
        {
            string Live2DPath = "Live2D\\";
            string Live2DExecutionName = "TCFace";

            Process Live2DProcess = Process.Start(@Live2DPath + Live2DExecutionName);
            CharaChange(LIVE2D_USR_DEFAULT_CHARA);
        }
    }
}

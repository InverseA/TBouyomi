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
        private void OpenLive2DWindow()
        {
            //==啟動exe==
            string Live2DPath = "Live2D\\";
            string Live2DExecutionName = "TBFace";

            Process Live2DProcess = Process.Start(@Live2DPath+Live2DExecutionName);
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
                string Volume_String;

                // Program is suspended while waiting for an incoming connection.
                //Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

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
                        Volume_String = VOLUME.ToString();
                        msg = System.Text.Encoding.ASCII.GetBytes(Volume_String);
                        //PutSystemMsg("Live2D Thread Send : " + Volume_String + "\n", Brushes.Blue);

                        handler.Send(msg);

                        Thread.Sleep(16);
                    }
                    catch(Exception ex)
                    {
                        PutSystemMsg("Live2D視窗關閉(棒讀醬需要稍作休息)" + "\n", Brushes.DarkRed);

                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();

                        break;
                    }
                }
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            catch (Exception ex)
            {
                //PutSystemMsg("2 Live2D視窗關閉，資源釋放中..." + "\n", Brushes.Red);
            }
        }
    }
}

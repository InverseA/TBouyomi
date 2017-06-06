using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Twitch_Bouyomi
{
    public class IrcClient
    {
        public NetworkStream stream;
        private TcpClient tcpClient;
        private StreamReader inputStream;
        private StreamWriter outputStream;
        private string userName;

        public IrcClient(string ip, int port, string userName, string password)
        {
            this.userName = userName;
            string temp;
            tcpClient = new TcpClient(ip, port);
            stream = tcpClient.GetStream();
            inputStream = new StreamReader(tcpClient.GetStream());
            outputStream = new StreamWriter(tcpClient.GetStream());

            if (!password.StartsWith("oauth:"))
            {
                password = "oauth:" + password;
            }

            outputStream.WriteLine("PASS " + password);
            outputStream.WriteLine("NICK " + userName);
            //outputStream.WriteLine("USER" + userName + " 8 * :" + userName);
            outputStream.Flush();
            outputStream.WriteLine("CAP REQ :twitch.tv/membership");
            outputStream.WriteLine("CAP REQ :twitch.tv/commands");
            joinRoom(userName);
        }

        public void joinRoom(string channel)
        {
            channel = channel.ToLower();
            outputStream.WriteLine("JOIN #" + channel);
            //outputStream.WriteLine("JOIN #" + channel);
            outputStream.Flush();
        }

        public void sendchatMessage(string message)
        {
            sendMessage(":" + userName + "!" + userName + "@" + userName + ".tmi.twitch.tv PRIVMSG #"
                + userName + " : " + message);
        }

        public void sendMessage(string message)
        {
            outputStream.WriteLine(message);
            outputStream.Flush();
        }

        public string readMessage()
        {
            string message = inputStream.ReadLine() + "\n";
            return message;
        }

        public void close_irc()
        {
            outputStream.Close();
            inputStream.Close();
            stream.Close();
            tcpClient.Close();
            outputStream.Dispose();
            inputStream.Dispose();
            stream.Dispose();
        }

        public void SendModListRequest()
        {
            outputStream.WriteLine("PRIVMSG "+ userName.ToLower()+" :.mods");
        }
    }
    //=============End of IrcClient Class=============

}

using System.Net.Sockets;
using System.IO;

namespace Twitch_Bouyomi
{
    public class IrcClient
    {
        const bool DEBUG_MODE = false;
        private string DEBUG_CHANNEL = "";

        public NetworkStream stream;
        private TcpClient tcpClient;
        private StreamReader inputStream;
        private StreamWriter outputStream;
        private string userName;

        public IrcClient(string ip, int port, string userName, string password)
        {
            this.userName = userName;
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
            outputStream.Flush();
            outputStream.WriteLine("CAP REQ :twitch.tv/membership");
            outputStream.WriteLine("CAP REQ :twitch.tv/tags");
            outputStream.WriteLine("CAP REQ :twitch.tv/commands");
            joinRoom(userName);
        }

        public void joinRoom(string channel)
        {
            channel = channel.ToLower();
            if (DEBUG_MODE)
            {
#pragma warning disable CS0162 // 偵測到不會執行到的程式碼
                outputStream.WriteLine("JOIN #" + DEBUG_CHANNEL);
#pragma warning restore CS0162 // 偵測到不會執行到的程式碼
            }
            else
                outputStream.WriteLine("JOIN #" + channel);
            outputStream.Flush();
        }

        public void sendchatMessage(string message)
        {
            if (DEBUG_MODE)
            {
#pragma warning disable CS0162 // 偵測到不會執行到的程式碼
                sendMessage(":" + userName + "!" + userName + "@" + userName + ".tmi.twitch.tv PRIVMSG #" + DEBUG_CHANNEL + " : " + message);
#pragma warning restore CS0162 // 偵測到不會執行到的程式碼
            }
            else
                sendMessage(":" + userName + "!" + userName + "@" + userName + ".tmi.twitch.tv PRIVMSG #" + userName + " : " + message);
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

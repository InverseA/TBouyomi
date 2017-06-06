using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        //Twitch kraken 的API更新後，需要驗證，因此暫時關閉觀看者數的功能。
        
        private void Channel_viewer()
        {
            
            string html;
            string url;
            string view_num;
            HttpWebRequest request;
            HttpWebResponse response;
            Stream resStream;
            StreamReader sr;

            url = "https://api.twitch.tv/kraken/streams/" + current_channel;
            //url = "https://api.twitch.tv/kraken/streams/" + current_channel;
            /*
            while (true)
            {
                html = null;

                request = null;
                response = null;
                resStream = null;
                sr = null;

                //tp://stackoverflow.com/questions/943852/how-to-send-an-https-get-request-in-c-sharp
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                    response = (HttpWebResponse)request.GetResponse();
                    resStream = response.GetResponseStream();
                    sr = new StreamReader(resStream);
                    html = sr.ReadToEnd();
                    sr.Close();
                    if( sr != null )
                        sr.Dispose();
                    resStream.Close();
                    if( resStream != null )
                        resStream.Dispose();
                    response.Close();
                    if( response != null )
                        response.Dispose();
                }
                catch (Exception ex)
                {
                    Push_A_message_to_Room("Error:" + ex.Message + "\n");
                }
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
                resStream.Close();
                if (resStream != null)
                    resStream.Dispose();
                response.Close();
                if (response != null)
                    response.Dispose();

                if (html.Contains("viewers"))
                {
                    int index = 0;
                    index = html.IndexOf("viewers");
                    index += 9;

                    view_num = html.Substring(index);
                    index = 0;
                    while (view_num[index] != ',')
                    {
                        index++;
                    }
                    view_num = view_num.Remove(index);

                    if (Viewer_num.Dispatcher.CheckAccess())
                    {
                        Viewer_num.Content = "● " + view_num;
                    }
                    else
                    {
                        Viewer_num.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            Viewer_num.Content = "● " + view_num;
                        }));
                    }
                }
                
                Thread.Sleep(3600000);
            }
            */
        }
    }
}

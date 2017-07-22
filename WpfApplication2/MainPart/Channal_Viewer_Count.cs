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
            /*
#pragma warning disable CS0168 // 已宣告變數 'html'，但從未使用過它。
            string html;
#pragma warning restore CS0168 // 已宣告變數 'html'，但從未使用過它。
            string url;
#pragma warning disable CS0168 // 已宣告變數 'view_num'，但從未使用過它。
            string view_num;
#pragma warning restore CS0168 // 已宣告變數 'view_num'，但從未使用過它。
#pragma warning disable CS0168 // 已宣告變數 'request'，但從未使用過它。
            HttpWebRequest request;
#pragma warning restore CS0168 // 已宣告變數 'request'，但從未使用過它。
#pragma warning disable CS0168 // 已宣告變數 'response'，但從未使用過它。
            HttpWebResponse response;
#pragma warning restore CS0168 // 已宣告變數 'response'，但從未使用過它。
#pragma warning disable CS0168 // 已宣告變數 'resStream'，但從未使用過它。
            Stream resStream;
#pragma warning restore CS0168 // 已宣告變數 'resStream'，但從未使用過它。
#pragma warning disable CS0168 // 已宣告變數 'sr'，但從未使用過它。
            StreamReader sr;
#pragma warning restore CS0168 // 已宣告變數 'sr'，但從未使用過它。

            url = "https://api.twitch.tv/kraken/streams/" + current_channel;
            //url = "https://api.twitch.tv/kraken/streams/" + current_channel;
            
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
                    PutSystemMsg("Error:" + ex.Message + "\n", Brushes.Red);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        //private Paragraph paragraph;
        //private Brush red; 
        private void Push_A_message_to_Room(string msg)
        {
            if (IRC_textbox.Dispatcher.CheckAccess())
            {
                IRC_textbox.AppendText(msg);
                IRC_textbox.ScrollToEnd();
                /*
                IRC_textRoom.AppendText(msg);
                IRC_textRoom.ScrollToEnd();
                */
            }
            else
            {
                IRC_textbox.Dispatcher.BeginInvoke((Action)(() =>
                {
                    IRC_textbox.AppendText(msg);
                    IRC_textbox.ScrollToEnd();
                    /*
                    IRC_textRoom.AppendText(msg);
                    IRC_textRoom.ScrollToEnd();
                    */
                }));
            }
        }

        public void PutSystemMsg(string _Msg, SolidColorBrush _color)
        {
            if (IRC_textbox.Dispatcher.CheckAccess())
            {
                TextRange _TxtRange = new TextRange(SystemMsg.Document.ContentEnd, SystemMsg.Document.ContentEnd);
                _TxtRange.Text = _Msg;
                _TxtRange.ApplyPropertyValue(TextElement.ForegroundProperty, _color);
                SystemMsg.ScrollToEnd();
            }
            else
            {
                SystemMsg.Dispatcher.BeginInvoke((Action)(() =>
                {
                    TextRange _TxtRange = new TextRange(SystemMsg.Document.ContentEnd, SystemMsg.Document.ContentEnd);
                    _TxtRange.Text = _Msg;
                    _TxtRange.ApplyPropertyValue(TextElement.ForegroundProperty, _color);
                    SystemMsg.ScrollToEnd();
                }));
            }

                
        }
    }
}

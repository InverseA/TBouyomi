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

        private void Push_A_message_to_Room(string _Msg)
        {
            if (IRC_textbox.Dispatcher.CheckAccess())
            {
                TextRange _TxtRange = new TextRange(IRC_textbox.Document.ContentEnd, IRC_textbox.Document.ContentEnd);
                _TxtRange.Text = _Msg;
                _TxtRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.LightGray);
                IRC_textbox.ScrollToEnd();
            }
            else
            {
                IRC_textbox.Dispatcher.BeginInvoke((Action)(() =>
                {
                    TextRange _TxtRange = new TextRange(IRC_textbox.Document.ContentEnd, IRC_textbox.Document.ContentEnd);
                    _TxtRange.Text = _Msg;
                    _TxtRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.LightGray);
                    IRC_textbox.ScrollToEnd();
                    //IRC_textbox.AppendText(_Msg);
                    //IRC_textbox.ScrollToEnd();
                }));
            }
        }
        private void Push_A_message_to_Room(string _Msg, SolidColorBrush _color)
        {
            if (IRC_textbox.Dispatcher.CheckAccess())
            {
                TextRange _TxtRange = new TextRange(IRC_textbox.Document.ContentEnd, IRC_textbox.Document.ContentEnd);
                _TxtRange.Text = _Msg;
                _TxtRange.ApplyPropertyValue(TextElement.ForegroundProperty, _color);
                IRC_textbox.ScrollToEnd();
            }
            else
            {
                IRC_textbox.Dispatcher.BeginInvoke((Action)(() =>
                {
                    TextRange _TxtRange = new TextRange(IRC_textbox.Document.ContentEnd, IRC_textbox.Document.ContentEnd);
                    _TxtRange.Text = _Msg;
                    _TxtRange.ApplyPropertyValue(TextElement.ForegroundProperty, _color);
                    IRC_textbox.ScrollToEnd();
                    //IRC_textbox.AppendText(_Msg);
                    //IRC_textbox.ScrollToEnd();
                }));
            }
        }

        public void PutSystemMsg(string _Msg, SolidColorBrush _color)
        {
            if (SystemMsg.Dispatcher.CheckAccess())
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

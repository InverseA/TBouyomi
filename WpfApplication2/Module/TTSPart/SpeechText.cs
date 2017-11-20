namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void SpeechTheText(TTS_object Package)      //最後發音部分
        {
            if (ISWIN7)
                Speech_win7(Package);
            else
                Speech_win8(Package);
        }
    }
}

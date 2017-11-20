namespace Twitch_Bouyomi
{
    internal class SubGroup
    {
        private char _string_1;
        private char _string_2;

        private int _distance1;
        private int _distance2;
        private bool _flag;

        public SubGroup(char string1, char string2)
        {
            _string_1 = string1;
            _string_2 = string2;
            _distance1 = 0;
            _distance2 = 0;
            _flag = false;
        }

        public bool Flag { get => _flag; set => _flag = value; }
        public int Distance1 { get => _distance1; set => _distance1 = value; }
        public int Distance2 { get => _distance2; set => _distance2 = value; }
        public char String_1 { get => _string_1; set => _string_1 = value; }
        public char String_2 { get => _string_2; set => _string_2 = value; }
    }
}

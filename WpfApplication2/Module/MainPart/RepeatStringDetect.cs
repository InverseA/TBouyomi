using System.Collections.Generic;

namespace Twitch_Bouyomi
{
    class RepeatStringDetect
    {
        private string _source;
        private string _afr;
        private bool _isRepeated;
        List<SubGroup> _subGroup;


        public RepeatStringDetect(string Source)
        {
            _source = Source;
            _isRepeated = false;

            _subGroup = new List<SubGroup>();
            _RepeatStringProc();
        }

        public string AfterProc { get => _afr; set => _afr = value; }
        public bool Repeated { get => _isRepeated; set => _isRepeated = value; }

        private void _RepeatStringProc()
        {
            for (int i = 0; i < _source.Length; i++)
            {
                SubGroup NewGup;

                _source = _source.Replace("\"", "");

                if ((i + 1) == _source.Length)
                {
                    NewGup = new SubGroup(_source[i], '\n');
                }
                else
                {
                    NewGup = new SubGroup(_source[i], _source[i + 1]);
                }

                if (NewGup != null)
                    _subGroup.Add(NewGup);
            }

            for (int i = 0; i < _subGroup.Count; i++)        //count distance from head
            {
                for (int j = i + 1; j < _subGroup.Count; j++)
                {
                    if ((_subGroup[i].String_1 == _subGroup[j].String_1) &&
                        (_subGroup[i].String_2 == _subGroup[j].String_2))
                    {
                        _subGroup[i].Distance1 = j - i;
                        break;
                    }
                }
            }

            _subGroup.Reverse();
            for (int i = 0; i < _subGroup.Count; i++)       //count distance from tail
            {
                for (int j = i + 1; j < _subGroup.Count; j++)
                {
                    if ((_subGroup[i].String_1 == _subGroup[j].String_1) &&
                        (_subGroup[i].String_2 == _subGroup[j].String_2))
                    {
                        _subGroup[i].Distance2 = j - i;
                        break;
                    }
                }
            }


            _subGroup.Reverse();
            for (int i = 0; i < _subGroup.Count; i++)
            {
                _subGroup[i].Distance2 = _subGroup[i].Distance1 + _subGroup[i].Distance2;
            }

            for (int i = 0; i < _subGroup.Count; i++)       //first check.
            {
                if (_subGroup[i].Distance1 > 0)
                {
                    if (((i + 2) < _subGroup.Count) &&
                    (_subGroup[i].Distance1 == _subGroup[i + 1].Distance1) &&
                    (_subGroup[i].Distance1 == _subGroup[i + 2].Distance1))
                    {
                        _isRepeated = true;
                        for (int j = 0; j < 3; j++)
                        {
                            int _d = _subGroup[i + j].Distance1;
                            _subGroup[i + j].Flag = true;
                            _subGroup[i + j + 1].Flag = true;
                            _subGroup[i + j + _d].Flag = true;
                            _subGroup[i + j + _d + 1].Flag = true;

                        }
                    }
                }

            }

            for (int i = 0; i < _subGroup.Count; i++)       //second check.
            {
                if (_subGroup[i].Distance2 > 0)
                {
                    if (((i + 2) < _subGroup.Count) &&
                    (_subGroup[i].Distance2 == _subGroup[i + 1].Distance2) &&
                    (_subGroup[i].Distance2 == _subGroup[i + 2].Distance2))
                    {
                        _isRepeated = true;

                        for (int j = 0; j < 3; j++)
                        {
                            int _d1 = _subGroup[i + j].Distance1;
                            int _d2 = _subGroup[i + j].Distance2 - _subGroup[i + j].Distance1;

                            _subGroup[i + j].Flag = true;
                            if ((i + j + 1) < _subGroup.Count)
                                _subGroup[i + j + 1].Flag = true;
                            if ((i + j + _d1) < _subGroup.Count)
                                _subGroup[i + j + _d1].Flag = true;
                            if ((i + j + _d1 + 1) < _subGroup.Count)
                                _subGroup[i + j + _d1 + 1].Flag = true;

                            _subGroup[i + j - _d2].Flag = true;
                        }


                    }
                }

            }

            _afr = "";

            for (int i = 0; i < _subGroup.Count; i++)
            {
                if (!_subGroup[i].Flag)
                    _afr += _subGroup[i].String_1;
            }



        }
    }
}

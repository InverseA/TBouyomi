using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Add_SE(string SEFileName)
        {
            SE NewSE = new SE(SEFileName);
            SEList.Add(NewSE);
        }

        private int SE_index(string SEName)
        {
            int index = 0;
            if (SEName != null)
            {
                for(int i = 0; i < SEList.Count; i++)
                {
                    if (SEList[i].GetWord().ToString() == SEName)
                        return index;
                    else
                        index++;
                }
            }
            return -1;
        }
    }


    public class SE
    {
        public string _word { get; set; }
        public string _SEFileName { get; set; }

        public SE(string SEFileName)
        {
            _SEFileName = SEFileName;
            _word = SEFileName.Remove(SEFileName.LastIndexOf("."));
        }

        public string GetWord()
        {
            return _word;
        }

        public string GetSEFileName()
        {
            return _SEFileName;
        }
    }
}

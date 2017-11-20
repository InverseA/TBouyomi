using System;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private static Object ExceptLock = new object();

        public void Add_Except(string name)
        {
            name = name.Replace("\n", "");
            try
            {
                Except NewExcept = new Except(name);
                lock (ExceptLock)
                {
                    ExceptList.Add(NewExcept);
                }
                if(!IsInit)
                    PutSystemMsg("已新增棒讀忽略帳號 ： " + name + "\n", Brushes.Green);
            }
            catch(Exception ex)
            {
                PutSystemMsg("Error 新增帳號忽略 : " + ex.Message + "\n", Brushes.Red);
            }
        }

        public void Delete_Except(string name)
        {
            name = name.Replace("\n", "");
            try
            {
                lock (ExceptLock)
                {
                    ExceptList.RemoveAll(x => x.GetName() == name);
                }
                PutSystemMsg("已移除棒讀忽略帳號 ： " + name + "\n", Brushes.Green);
            }
            catch (Exception ex)
            {
                PutSystemMsg("Error 移除帳號忽略 : " + ex.Message + "\n", Brushes.Red);
            }
        }

        private int Except_Index(string name)
        {
            if (ExceptList == null)
                return -1;
            int index = 0;

            if (name != "" || name != null)
            {
                for (int i = 0; i < ExceptList.Count; i++)
                {
                    if (ExceptList[i].GetName().ToString() == name)
                        return index;
                    else
                        index++;
                }
            }
            return -1;
        }
        
        public void ExceptListReverse()
        {
            ExceptList.Reverse();
        }
    }
    
public class Except
    {
        public string name { get; set; }

        public Except(string name)
        {
            this.name = name.Replace("\n", null);
        }

        public string GetName()
        {
            return this.name;
        }
    }
}

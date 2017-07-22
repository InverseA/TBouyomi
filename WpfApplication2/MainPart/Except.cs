using System;
using System.Windows.Media;

namespace Twitch_Bouyomi
{
    public partial class MainWindow
    {
        private void Add_Except(string name)
        {
            name = name.Replace("\n", "");
            try
            {
                Except NewExcept = new Except(name);
                ExceptList.Add(NewExcept);
                PutSystemMsg("已新增棒讀忽略帳號 ： " + name + "\n", Brushes.Green);
            }
            catch(Exception ex)
            {
                PutSystemMsg("Error 忽略帳號 : " + ex.Message + "\n", Brushes.Red);
            }
        }
        
        private void Delete_Except(string name)
        {
            name = name.Replace("\n", "");
            try
            {
                ExceptList.RemoveAll(x => x.GetName() == name);
                PutSystemMsg("已移除棒讀忽略帳號 ： " + name + "\n", Brushes.Green);
            }
            catch (Exception ex)
            {
                PutSystemMsg("Error 忽略帳號 : " + ex.Message + "\n", Brushes.Red);
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
    }
    
class Except
    {
        private string name = null;

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

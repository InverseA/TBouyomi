using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Twitch_Bouyomi.View
{
    /// <summary>
    /// DataView_IgnoreAccount.xaml 的互動邏輯
    /// </summary>
    public partial class DataView_IgnoreAccount : UserControl
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        List<Except> SelectObjectList = null;

        public DataView_IgnoreAccount()
        {
            InitializeComponent();
            MakeExceptView();
        }

        private void MakeExceptView()
        {
            CollectionViewSource ExceptView = null;
            ExceptView = new CollectionViewSource();
            if(ExceptView != null)
            {
                ExceptView.Source = window.GetExceptList();
                ExceptViewList.DataContext = ExceptView;
            }
        }

        private void ExceptViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectObjectList = new List<Except>();
            var ExceptViewList_SelectedItem = ExceptViewList.SelectedItems;
            foreach (Except _ExceptViewObject in ExceptViewList_SelectedItem)
            {
                SelectObjectList.Add(_ExceptViewObject);
            }
        }

        private void AddNewExcept(object sender, RoutedEventArgs e)
        {
            AddExcept cc = new AddExcept();
            cc.Owner = Window.GetWindow(this);
            if (cc.ShowDialog() == true)
            {
                if(cc.AddAcc.Text.ToString() != "")
                {
                    window.Add_Except(cc.AddAcc.Text.ToString());
                    MakeExceptView();
                    window.WriteExceptFile();
                }
            }
        }

        private void DeleteExcept(object sender, RoutedEventArgs e)
        {
            DeleteConfirm cc = new DeleteConfirm();
            cc.Owner = Window.GetWindow(this);
            if (SelectObjectList != null && SelectObjectList.Count > 0)
            {
                if (cc.ShowDialog() == true)
                {
                    for (int i = 0; i < SelectObjectList.Count; i++)
                    {
                        window.Delete_Except(SelectObjectList[i].name);
                    }
                }
            }
            MakeExceptView();
            window.WriteExceptFile();
        }
    }
}

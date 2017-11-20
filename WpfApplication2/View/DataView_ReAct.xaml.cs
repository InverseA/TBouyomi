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
    /// DataView_ReAct.xaml 的互動邏輯
    /// </summary>
    public partial class DataView_ReAct : UserControl
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        List<ReAct> SelectObjectList = null;

        public DataView_ReAct()
        {
            InitializeComponent();
            MakeReactView();
        }

        private void MakeReactView()
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            CollectionViewSource ReactView = new CollectionViewSource();
            ReactView.Source = window.GetReActList();
            ReactViewList.DataContext = ReactView;
        }

        private void ReactViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectObjectList = new List<ReAct>();
            var ReactViewList_SelectedItem = ReactViewList.SelectedItems;
            foreach (ReAct _ReactViewObject in ReactViewList_SelectedItem)
            {
                SelectObjectList.Add(_ReactViewObject);
            }
        }

        private void AddNewReAct(object sender, RoutedEventArgs e)
        {
            AddAct cc = new AddAct();
            cc.Owner = Window.GetWindow(this);
            if (cc.ShowDialog() == true)
            {
                if (cc.WordBefore.Text.ToString() != ""
                    && cc.WordAfter.Text.ToString() != "")
                {
                    window.Add_ReAct(cc.WordBefore.Text.ToString(),
                                            cc.WordAfter.Text.ToString());
                    MakeReactView();
                    window.WriteReActFile();
                }
            }
        }

        private void DeleteReAct(object sender, RoutedEventArgs e)
        {
            DeleteConfirm cc = new DeleteConfirm();
            cc.Owner = Window.GetWindow(this);
            if (SelectObjectList != null && SelectObjectList.Count > 0)
            {
                if (cc.ShowDialog() == true)
                {
                    for (int i = 0; i < SelectObjectList.Count; i++)
                    {
                        window.Delete_ReAct(SelectObjectList[i].word);
                    }
                }
            }
            MakeReactView();
            window.WriteReActFile();
        }
    }
}

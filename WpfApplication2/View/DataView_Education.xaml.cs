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
    /// DataView_Education.xaml 的互動邏輯
    /// </summary>
    public partial class DataView_Education : UserControl
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        List<Education> SelectObjectList = null;

        public DataView_Education()
        {
            InitializeComponent();
            MakeEducationView();
        }

        private void MakeEducationView()
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            CollectionViewSource EduView = new CollectionViewSource();
            EduView.Source = window.GetEducationList();
            EducationViewList.DataContext = EduView;
        }

        private void AddNewEducation(object sender, RoutedEventArgs e)
        {
            AddEducation cc = new AddEducation();
            cc.Owner = Window.GetWindow(this);
            if (cc.ShowDialog() == true)
            {
                if(cc.WordBefore.Text.ToString() !="" 
                    && cc.WordAfter.Text.ToString() != "")
                {
                    window.Add_Education(cc.WordBefore.Text.ToString(),
                                            cc.WordAfter.Text.ToString());
                    MakeEducationView();
                    window.WriteEducationFile();
                }
            }
        }

        private void DeleteEducation(object sender, RoutedEventArgs e)
        {
            DeleteConfirm cc = new DeleteConfirm();
            cc.Owner = Window.GetWindow(this);
            if (SelectObjectList != null && SelectObjectList.Count > 0)
            {
                if (cc.ShowDialog() == true)
                {
                    for (int i = 0; i < SelectObjectList.Count; i++)
                    {
                        window.Delete_Education(SelectObjectList[i].word);
                    }
                }
            }
            MakeEducationView();
            window.WriteEducationFile();
        }

        private void EducationViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectObjectList = new List<Education>();
            var EducationtViewList_SelectedItem = EducationViewList.SelectedItems;
            foreach(Education _EducationViewObject in EducationtViewList_SelectedItem)
            {
                SelectObjectList.Add(_EducationViewObject);
            }
        }
    }
}

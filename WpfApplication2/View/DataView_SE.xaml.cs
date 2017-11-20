using NAudio.Wave;
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
    /// DataView_SE.xaml 的互動邏輯
    /// </summary>
    public partial class DataView_SE : UserControl
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        List<SE> SelectObjectList = null;

        public DataView_SE()
        {
            InitializeComponent();
            MakeSEView();
        }

        private void MakeSEView()
        {
            CollectionViewSource EduView = new CollectionViewSource();
            EduView.Source = window.GetSEList();
            SEViewList.DataContext = EduView;
        }

        private void ReReadSE(object sender, RoutedEventArgs e)
        {
            window.ReScanSEFolder();
            MakeSEView();
        }

        private void SEViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectObjectList = new List<SE>();
            var SEViewList_SelectedItem = SEViewList.SelectedItems;
            foreach (SE _SEViewObject in SEViewList_SelectedItem)
            {
                SelectObjectList.Add(_SEViewObject);
            }
        }

        private void PreviewSE(object sender, RoutedEventArgs e)
        {
            if(SelectObjectList != null && SelectObjectList.Count > 0)
            {
                try
                {
                    string _SEPath = "SE";
                    WaveStream _MainOutputStream = null;
                    WaveChannel32 _volumeStream = null;
                    WaveOutEvent _player = null;
                    if (_player != null)     //初始化_player
                    {
                        _player.Stop();
                        _player.Dispose();
                        _player = null;
                        _volumeStream.Dispose();
                        _volumeStream = null;
                        _MainOutputStream.Dispose();
                        _MainOutputStream = null;
                    }
                    _MainOutputStream = new Mp3FileReader(_SEPath + "/" + SelectObjectList[0]._SEFileName);
                    _volumeStream = new WaveChannel32(_MainOutputStream);
                    _player = new WaveOutEvent();
                    _player.Init(_volumeStream);
                    _player.Volume = (float)window.Get_SE_Volume() / 100;
                    _player.Play();

                    _player = null;
                }
                catch (Exception ex)
                {
                    window.PutSystemMsg("SE Error:" + ex.Message + "\n", Brushes.Red);
                }
            }
            
        }
    }
}

using System.Windows.Controls;
using WinterOlympics2014WP.Models;
using System.Windows;
using WinterOlympics2014WP.Utility;
using System;

namespace WinterOlympics2014WP.Controls
{
    public partial class LivePageItemAlbum : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemAlbum()
        {
            InitializeComponent();
        }

        private void UserControl_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage!=null)
            {
                LiveLineItem item = sender.GetDataContext<LiveLineItem>();
                if (item!=null)
                {
                    //TO-DO : pass correct album id
                    string naviString = string.Format("/Pages/AlbumPage.xaml?{0}={1}", NaviParam.ALBUM_ID, "d89e0b65d8946fbe");
                    HostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                }
            }
        }
    }
}

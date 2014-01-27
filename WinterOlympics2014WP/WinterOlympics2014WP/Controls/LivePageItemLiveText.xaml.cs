using System;
using System.Windows.Controls;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Controls
{
    public partial class LivePageItemLiveText : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemLiveText()
        {
            InitializeComponent();
            this.Tap += LivePageItemLiveText_Tap;
        }

        void LivePageItemLiveText_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage != null)
            {
                LiveLineItem item = sender.GetDataContext<LiveLineItem>();
                if (item != null)
                {
                    if (item.Type == 1)
                    {
                        string naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}", NaviParam.NEWS_ID, item.ID);
                        HostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    }
                }
            }
        }
    }
}

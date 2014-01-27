using System.Windows.Controls;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Pages;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Controls
{
    public partial class LivePageItemVideo : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemVideo()
        {
            InitializeComponent();
        }

        private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage != null)
            {
                LiveLineItem item = sender.GetDataContext<LiveLineItem>();
                if (item != null)
                {
                    VideoPage.PlayVideo(HostingPage, item.ID, this.snow1);
                }
            }
        }
    }
}

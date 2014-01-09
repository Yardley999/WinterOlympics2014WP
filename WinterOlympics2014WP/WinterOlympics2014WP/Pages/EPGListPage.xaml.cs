using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WinterOlympics2014WP.Pages
{
    public partial class EPGListPage : PhoneApplicationPage
    {
        #region Lifecycle

        public EPGListPage()
        {
            InitializeComponent();
            InitEpgList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PopulateEPGList();

            bool xx = App.Current.Host.Content.ScaleFactor == 100;
        }

        #endregion

        #region EPG List

        private void InitEpgList()
        {
            epgList.HostingPage = this;
            epgList.QuickSelector = this.quickSelector;
        }

        private void PopulateEPGList()
        {
            epgList.PopulateData(DateTime.Today);
        }

        private void EpgItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/LivePage.xaml", UriKind.Relative));
        }

        #endregion

    }
}
using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;

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
            //TO-DO : pass param to specify date
            epgList.LoadEpg(DateTime.Today);
        }

        #endregion

    }
}
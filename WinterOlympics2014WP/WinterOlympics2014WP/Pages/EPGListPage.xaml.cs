using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Pages
{
    public partial class EPGListPage : PhoneApplicationPage
    {
        #region Property

        private string gameDate = string.Empty;

        #endregion

        #region Lifecycle

        public EPGListPage()
        {
            InitializeComponent();
            InitEpgList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameDate = NavigationContext.QueryString[NaviParam.CALENDAR_DATE];
            PopulateEPGList(gameDate);
        }

        #endregion

        #region EPG List

        private void InitEpgList()
        {
            epgList.HostingPage = this;
            epgList.QuickSelector = this.quickSelector;
        }

        private void PopulateEPGList(string date)
        {
            epgList.LoadEpg(date);
        }

        #endregion

    }
}
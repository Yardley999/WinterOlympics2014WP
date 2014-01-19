using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Pages
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateLocalFolderSize();
        }

        #region Settings

        private async void UpdateLocalFolderSize()
        {
            var size = await IsolatedStorageHelper.GetUserDataSize();
            localFolderSizeTextBlock.Text = Math.Round(((double)size / 1048576d), 2).ToString() + " MB";
        }

        #endregion

        private async void ClearCacheButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await IsolatedStorageHelper.ClearUserData();
            UpdateLocalFolderSize();
        }
    }
}
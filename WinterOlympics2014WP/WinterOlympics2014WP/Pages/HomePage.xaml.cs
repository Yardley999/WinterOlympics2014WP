using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace WinterOlympics2014WP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        public HomePage()
        {
            InitializeComponent();
            BuildApplicationBar();
            SetSplashImage();
            InitQuickSelector();
        }

        #region Set Splash Image

        private void SetSplashImage()
        {
            SetDefualtSplashImage();
        }

        private void SetDefualtSplashImage()
        {
            this.splashImage.Source = new BitmapImage(new Uri("/Assets/Images/SplashScreenDefault.PNG", UriKind.Relative));
        }

        #endregion

        #region Quick Selector

        bool quickSelectorShown = false;
        Dictionary<string, bool> hoursOfDay = new Dictionary<string, bool>();
        List<string> hoursList = new List<string>();

        private void InitQuickSelector()
        {
            hoursOfDay.Add("00:00", true);
            hoursOfDay.Add("01:00", false);
            hoursOfDay.Add("02:00", true);
            hoursOfDay.Add("03:00", true);
            hoursOfDay.Add("04:00", false);
            hoursOfDay.Add("05:00", false);
            hoursOfDay.Add("06:00", false);
            hoursOfDay.Add("07:00", false);
            hoursOfDay.Add("08:00", false);
            hoursOfDay.Add("09:00", false);
            hoursOfDay.Add("10:00", false);
            hoursOfDay.Add("11:00", false);
            hoursOfDay.Add("12:00", false);
            hoursOfDay.Add("13:00", false);
            hoursOfDay.Add("14:00", false);
            hoursOfDay.Add("15:00", false);
            hoursOfDay.Add("16:00", false);
            hoursOfDay.Add("17:00", false);
            hoursOfDay.Add("18:00", false);
            hoursOfDay.Add("19:00", true);
            hoursOfDay.Add("20:00", true);
            hoursOfDay.Add("21:00", true);
            hoursOfDay.Add("22:00", true);
            hoursOfDay.Add("23:00", true);
            quickSelector.SetItems(hoursOfDay);

            hoursList = hoursOfDay.Keys.ToList();
        }

        private void QuickSelector_SelectionChanged(object sender, int selectedIndex)
        {
            //MessageBox.Show(hoursList[selectedIndex]);
        }

        private void ShowQuickSelector()
        {
            if (!quickSelectorShown)
            {
                quickSelectorShown = true;
                VisualStateManager.GoToState(this, "QuickSelectorShown", true);
            }
        }

        private void HideQuickSelector()
        {
            if (quickSelectorShown)
            {
                quickSelectorShown = false;
                VisualStateManager.GoToState(this, "QuickSelectorHidden", true);
            }
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarRefreshHome;
        ApplicationBarIconButton appBarRefreshNews;
        ApplicationBarMenuItem appBarSetting;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh home
            appBarRefreshHome = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefreshHome.Text = "刷新";

            // refresh news
            appBarRefreshNews = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefreshNews.Text = "刷新";

            appBarSetting = new ApplicationBarMenuItem("设置");

            SetAppBarForSplash();
        }

        private void ClearAppBar()
        {
            ApplicationBar.Buttons.Clear();
            ApplicationBar.MenuItems.Clear();
        }

        private void SetAppBarForSplash()
        {
            ClearAppBar();
            ApplicationBar.MenuItems.Add(appBarSetting);
            ApplicationBar.Mode = ApplicationBarMode.Minimized;
        }

        private void SetAppBarForHome()
        {
            ClearAppBar();
            ApplicationBar.Buttons.Add(appBarRefreshHome);
            ApplicationBar.MenuItems.Add(appBarSetting);
            ApplicationBar.Mode = ApplicationBarMode.Default;
        }

        private void SetAppBarForNews()
        {
            ClearAppBar();
            ApplicationBar.Buttons.Add(appBarRefreshNews);
            ApplicationBar.MenuItems.Add(appBarSetting);
            ApplicationBar.Mode = ApplicationBarMode.Default;
        }

        private void SetAppBarForMore()
        {
            ClearAppBar();
            ApplicationBar.MenuItems.Add(appBarSetting);
            ApplicationBar.Mode = ApplicationBarMode.Minimized;
        }

        #endregion

        #region Panorama Selection

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (panorama.SelectedIndex)
            {
                case 0:
                    SetAppBarForSplash();
                    HideQuickSelector();
                    break;
                case 1:
                    SetAppBarForHome();
                    ShowQuickSelector();
                    break;
                case 2:
                    SetAppBarForNews();
                    HideQuickSelector();
                    break;
                case 3:
                    SetAppBarForMore();
                    HideQuickSelector();
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
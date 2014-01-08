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
using System.Collections.ObjectModel;

namespace WinterOlympics2014WP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        #region Lifecycle

        public HomePage()
        {
            InitializeComponent();
            BuildApplicationBar();
            SetSplashImage();
            InitQuickSelector();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PopulateToday();
            LoadNews();
        }

        #endregion

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
            todayItemsControl.ScrollIntoView(6);
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

        #region Today

        ObservableCollection<int> todayList = new ObservableCollection<int>();

        private void PopulateToday()
        {
            todayList.Clear();

            todayList.Add(0);
            todayList.Add(1);
            todayList.Add(2);
            todayList.Add(3);
            todayList.Add(4);
            todayList.Add(5);
            todayList.Add(6);
            todayList.Add(7);
            todayList.Add(8);
            todayList.Add(9);
            todayItemsControl.ItemsSource = todayList;
        }

        #endregion

        #region News

        ObservableCollection<int> newsList = new ObservableCollection<int>();

        private void LoadNews()
        {
            newsList.Clear();

            newsList.Add(0);
            newsList.Add(1);
            newsList.Add(2);
            newsList.Add(3);
            newsList.Add(4);
            newsList.Add(5);
            newsList.Add(6);
            newsList.Add(7);
            newsList.Add(8);
            newsList.Add(9);

            newsListBox.ItemsSource = newsList;
        }

        private void NewsItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/NewsDetailPage.xaml", UriKind.Relative));
        }

        #endregion

        #region More Section

        private void medalTallyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MedalTallyPage.xaml", UriKind.Relative));
        }

        #endregion



    }
}
﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;
using WinterOlympics2014WP.Animations;

namespace WinterOlympics2014WP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        #region Property

        #endregion

        #region Lifecycle

        public HomePage()
        {
            InitializeComponent();
            BuildApplicationBar();
            InitEpgList();
            //newsListBox.ItemsSource = newsList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadSplashImage();
            LoadEpg();
            LoadNews();

            fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
        }

        #endregion

        #region Splash

        DataLoader<Splash> splashLoader = new DataLoader<Splash>();
        ImageHelper imageHelperSplash = new ImageHelper();

        private void LoadSplashImage()
        {
            DisplayLocalSplashImage();

            if (splashLoader.Loaded || splashLoader.Busy)
            {
                return;
            }

            bigSnow.IsBusy = true;

            splashLoader.LoadWithoutCaching("getsplash",
                splash =>
                {
                    if (splash != null)
                    {
                        imageHelperSplash.Download(splash.Image, Constants.SPLASH_MODULE, Constants.SPLASH_FILE_NAME, SplashDownLoadCallback);
                    }
                });
        }

        private void SplashDownLoadCallback()
        {
            Dispatcher.BeginInvoke(() =>
            {
                DisplayLocalSplashImage();
                bigSnow.IsBusy = false;
            });
        }

        private async void DisplayLocalSplashImage()
        {
            BitmapImage source = await imageHelperSplash.ReadImage(Constants.SPLASH_MODULE, Constants.SPLASH_FILE_NAME);
            this.splashImage.Source = source;
        }

        private void splashImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bigSnow.IsBusy = !bigSnow.IsBusy;
            //snow.IsBusy = !snow.IsBusy;
        }

        #endregion

        #region Quick Selector

        bool quickSelectorShown = false;

        private void ShowQuickSelector()
        {
            if (!quickSelectorShown)
            {
                quickSelectorShown = true;
                VisualStateManager.GoToState(this, "QuickSelectorShown", true);
            }
            newsListScrollViewer.IsHitTestVisible = false;
        }

        private void HideQuickSelector()
        {
            if (quickSelectorShown)
            {
                quickSelectorShown = false;
                VisualStateManager.GoToState(this, "QuickSelectorHidden", true);
            }
            newsListScrollViewer.IsHitTestVisible = true;
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
            appBarSetting.Click += appBarSetting_Click;

            SetAppBarForSplash();
        }

        void appBarSetting_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SettingsPage.xaml", UriKind.Relative));
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

        private void InitEpgList()
        {
            epgList.HostingPage = this;
            epgList.QuickSelector = this.quickSelector;
        }

        private void LoadEpg()
        {
            //TO-DO : get today instead of test date
            DateTime today = new DateTime(2014, 2, 8);
            epgList.LoadEpg(today);
        }

        #endregion

        #region News

        ListDataLoader<News> newsLoader = new ListDataLoader<News>();

        private void LoadNews()
        {
            if (newsLoader.Loaded || newsLoader.Busy)
            {
                return;
            }

            newsLoader.Load("getnewslist", string.Empty, true, Constants.NEWS_MODULE, Constants.NEWS_FILE_NAME,
                list =>
                {
                    if (list != null)
                    {
                        newsListBox.ItemsSource = list;
                    }
                });
        }

        private void NewsItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/NewsDetailPage.xaml", UriKind.Relative));
        }

        #endregion

        #region More Section

        private void medalTallyButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Navigate("/Pages/MedalTallyPage.xaml");
        }

        private void calendarButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Navigate("/Pages/CalendarPage.xaml");
        }

        private void categoryButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Navigate("/Pages/CategoryListPage.xaml");
        }

        private void stadiumButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Navigate("/Pages/StadiumListPage.xaml");
        }

        private void subscribeButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Navigate("/Pages/MySubscriptionPage.xaml");
        }

        #endregion

        #region Page Navigation Transition

        FadeAnimation fadeAnimation = new FadeAnimation();

        private void Navigate(string uriString)
        {
            fadeAnimation.InstanceFade(this.contentPanel, 1d, 0d, Constants.NAVIGATION_DURATION, 
                fe =>
                {
                    NavigationService.Navigate(new Uri(uriString, UriKind.Relative));
                });
        }

        #endregion

    }
}
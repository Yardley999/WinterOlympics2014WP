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
using System.IO;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;

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
            InitEpgList();
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

        private void PopulateToday()
        {
            epgList.PopulateData(DateTime.Today);
        }

        #endregion

        #region News

        ObservableCollection<int> newsList = new ObservableCollection<int>();

        private void LoadNews_old()
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

        private void LoadNews()
        {
            try
            {
                String url = "http://115.28.21.97/api/server?cmd=getnewslist";
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
                request.Method = "GET";
                request.BeginGetResponse(GetNewsList_Callback, request);
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
        }

        private void GetNewsList_Callback(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;//获取异步操作返回的的信息
            WebResponse response = request.EndGetResponse(result);//结束对 Internet 资源的异步请求

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                List<News> newsList = JsonSerializer.Deserialize<List<News>>(json);
               
                //通过呼叫UI Thread来改变页面的显示
                Dispatcher.BeginInvoke(() => 
                { 
                    //httpWebRequestTextBlock.Text = contents.ToString().Substring(begin + 7, end - begin - 7); textBox2.Text = note; 
                });

            }
        }

        private void NewsItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/NewsDetailPage.xaml", UriKind.Relative));
        }

        #endregion

        #region More Section

        private void medalTallyButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MedalTallyPage.xaml", UriKind.Relative));
        }

        private void calendarButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/CalendarPage.xaml", UriKind.Relative));
        }

        private void categoryButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/CategoryListPage.xaml", UriKind.Relative));
        }

        private void stadiumButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/StadiumListPage.xaml", UriKind.Relative));
        }

        private void subscribeButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MySubscriptionPage.xaml", UriKind.Relative));
        }

        #endregion


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;

namespace WinterOlympics2014WP.Pages
{
    public partial class LivePage : PhoneApplicationPage
    {
        #region Lifecycle

        public LivePage()
        {
            InitializeComponent();
            descriptionListBox.ItemsSource = descriptionList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateDescriptionList();
        }

        #endregion

        private void Video_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void Albumn_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AlbumPage.xaml", UriKind.Relative));
        }

        #region Description

        ObservableCollection<string> descriptionList = new ObservableCollection<string>();

        private void UpdateDescriptionList()
        {
            descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
        }

        #endregion
    }
}
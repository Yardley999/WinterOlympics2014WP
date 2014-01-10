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
    public partial class StadiumListPage : PhoneApplicationPage
    {
        #region Lifecycle

        public StadiumListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadStadiums();
        }

        #endregion

        #region Load StadiumList

        ObservableCollection<int> stadiumList = new ObservableCollection<int>();

        private void LoadStadiums()
        {
            stadiumList.Clear();

            stadiumList.Add(0);
            stadiumList.Add(1);
            stadiumList.Add(2);
            stadiumList.Add(3);
            stadiumList.Add(4);
            stadiumList.Add(5);
            stadiumList.Add(6);
            stadiumList.Add(7);
            stadiumList.Add(8);
            stadiumList.Add(9);

            stadiumListBox.ItemsSource = stadiumList;
        }

        #endregion

        private void Stadium_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/StadiumDetailPage.xaml", UriKind.Relative));
        }
    }
}
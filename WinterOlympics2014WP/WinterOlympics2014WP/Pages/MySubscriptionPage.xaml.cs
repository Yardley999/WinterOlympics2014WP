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
    public partial class MySubscriptionPage : PhoneApplicationPage
    {
        #region Lifecycle

        public MySubscriptionPage()
        {
            InitializeComponent();
            scheduleListBox.ItemsSource = scheduleList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadScheduleList();
        }

        #endregion

        #region Subscription List

        ObservableCollection<int> scheduleList = new ObservableCollection<int>();

        private void LoadScheduleList()
        {
            scheduleList.Clear();

            scheduleList.Add(0);
            scheduleList.Add(1);
            scheduleList.Add(2);
            scheduleList.Add(3);
            scheduleList.Add(4);
            scheduleList.Add(5);
            scheduleList.Add(6);
            scheduleList.Add(7);
            scheduleList.Add(8);
            scheduleList.Add(9);
        }

        #endregion

        private void UnSubscribe_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void ExpandSchedule_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

    }
}
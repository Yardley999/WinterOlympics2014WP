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
using WinterOlympics2014WP.Animations;

namespace WinterOlympics2014WP.Pages
{
    public partial class CalendarPage : PhoneApplicationPage
    {
        #region Property

        #endregion

        #region Lifecycle

        public CalendarPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadDays();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.Uri.OriginalString != "app://external/")
            {
                HidePage();
            }
        }

        #endregion

        #region Load Calendar

        ObservableCollection<int> newsDays = new ObservableCollection<int>();

        private void LoadDays()
        {
            newsDays.Clear();

            newsDays.Add(0);
            newsDays.Add(1);
            newsDays.Add(2);
            newsDays.Add(3);
            newsDays.Add(4);
            newsDays.Add(5);
            newsDays.Add(6);
            newsDays.Add(7);
            newsDays.Add(8);
            newsDays.Add(9);

            daysListBox.ItemsSource = newsDays;
            ShowPage();
        }

        #endregion

        #region Page Navigation Transition

        FadeAnimation fadeAnimation = new FadeAnimation();
        MoveAnimation moveAnimation = new MoveAnimation();

        private void ShowPage()
        {
            contentPanel.UpdateLayout();
            moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 90, 0, 0, Constants.NAVIGATION_DURATION, null);
            fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
        }

        private void HidePage()
        {
            moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 0, 0, 90, Constants.NAVIGATION_DURATION, null);
            fadeAnimation.InstanceFade(this.contentPanel, 1d, 0d, Constants.NAVIGATION_DURATION, null);
        }

        #endregion

        private void Day_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/EPGListPage.xaml", UriKind.Relative));
        }

    }
}
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WinterOlympics2014WP.Utility;
using WinterOlympics2014WP.DataContext;
using WinterOlympics2014WP.Models;
using Microsoft.Phone.Shell;

namespace WinterOlympics2014WP.Pages
{
    public partial class MySubscriptionPage : PhoneApplicationPage
    {
        #region Lifecycle

        public MySubscriptionPage()
        {
            InitializeComponent();
            BuildApplicationBar();

            scheduleListBox.ItemsSource = scheduleList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadScheduleList();
        }

        #endregion

        #region Subscription List

        ObservableCollection<GameSchedule> scheduleList = new ObservableCollection<GameSchedule>();

        private void LoadScheduleList()
        {
            scheduleList.Clear();
            var list = SubscriptionDataContext.Current.LoadSubscriptions();
            foreach (var item in list)
            {
                scheduleList.Add(item);
            }
        }

        #endregion

        #region UnSubscribe

        private void UnSubscribe_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            GameSchedule schedule = sender.GetDataContext<GameSchedule>();
            if (schedule!=null)
            {
                ReminderHelper.RemoveReminder(schedule.ID);
            }
            SubscriptionDataContext.Current.RemoveSubscription(schedule.ID);

            scheduleList.Remove(schedule);

            toast.ShowMessage("成功取消预约。");
        }

        #endregion

        #region App Bar

        ApplicationBarMenuItem appBarClear;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            //clear
            appBarClear = new ApplicationBarMenuItem("清空");
            appBarClear.Click += appBarClear_Click;
            ApplicationBar.MenuItems.Add(appBarClear);
        }

        void appBarClear_Click(object sender, System.EventArgs e)
        {
            ReminderHelper.ClearReminders();
            SubscriptionDataContext.Current.ClearSubscriptions();
            scheduleList.Clear();
            toast.ShowMessage("成功取消全部预约。");
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WinterOlympics2014WP.Utility;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Controls;
using System.Windows.Input;
using WinterOlympics2014WP.DataContext;
using System.Linq;

namespace WinterOlympics2014WP.Pages
{
    public partial class CategoryPage : PhoneApplicationPage
    {
        #region Property

        private string categoryID = string.Empty;

        #endregion

        #region Lifecycle

        public CategoryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            categoryID = NavigationContext.QueryString[NaviParam.CATEGORY_ID];
            SetTitle(categoryID);
            LoadSchedules();
        }

        #endregion

        #region Sub Title

        private void SetTitle(string cateId)
        {
            this.topBar.SecondaryHeader = "短道速滑";
        }

        #endregion

        #region VirtualizingStackPanel

        StackPanel scheduleItemsPanel = null;
        private void scheduleListBoxItemsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            scheduleItemsPanel = sender as StackPanel;
        }

        #endregion

        #region Schedule List

        ListDataLoader<GameSchedule> scheduleloader = new ListDataLoader<GameSchedule>();
        List<GameSchedule> scheduleList = new List<GameSchedule>();

        private void LoadSchedules()
        {
            if (scheduleloader.Loaded || scheduleloader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            scheduleloader.Load("getschedule", "&id=" + categoryID,true, Constants.SCHEDULE_MODULE, string.Format(Constants.SCHEDULE_FILE_NAME_FORMAT, categoryID),
                list =>
                {
                    var subscriptionList = GetSubscriptionList();
                    foreach (var item in list)
                    {
                        if (subscriptionList.Any(x => x.ID == item.ID))
                        {
                            item.Subscribed = true;
                        }
                        item.ArrowImage = "/Assets/Images/ArrowDown.png";
                    }

                    scheduleList = list;
                    this.scheduleListBox.ItemsSource = scheduleList;
                    snow1.IsBusy = false;
                });
        }

        private List<GameSchedule> GetSubscriptionList()
        {
            return SubscriptionDataContext.Current.LoadSubscriptions();
        }

        #endregion

        #region Result

        DataLoader<GameResult> resultLoader = new DataLoader<GameResult>();
        GameResultPanel gameResultPanel = null;
        GameSchedule expandedSchedule = null;

        private void LoadGameResult(GameSchedule schedule)
        {
            if (resultLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            resultLoader.Load("getresult", "&id=" + schedule.ID,true, Constants.SCHEDULE_MODULE, string.Format(Constants.RESULT_FILE_NAME_FORMAT, schedule.ID),
                list =>
                {
                    ShowResultPanel(schedule, list);
                    snow1.IsBusy = false;
                });
        }

        private void Schedule_Tap(object sender, GestureEventArgs e)
        {
            var schedule = sender.GetDataContext<GameSchedule>();
            if (schedule == expandedSchedule)
            {
                HideResultPanel();
            }
            else
            {
                if (expandedSchedule != null)
                {
                    HideResultPanel();
                }
                LoadGameResult(schedule);
            }
        }

        private void ShowResultPanel(GameSchedule schedule, GameResult result)
        {
            if (gameResultPanel == null)
            {
                gameResultPanel = new GameResultPanel();
            }
            if (scheduleItemsPanel.Children.Contains(gameResultPanel))
            {
                scheduleItemsPanel.Children.Remove(gameResultPanel);
            }

            gameResultPanel.DataContext = result;

            int index = scheduleList.IndexOf(schedule);
            scheduleItemsPanel.Children.Insert(index + 1, gameResultPanel);
            gameResultPanel.Show(result.RankList.Length > 0);
            schedule.ArrowImage = "/Assets/Images/ArrowUp.png";
            expandedSchedule = schedule;
        }

        private void HideResultPanel()
        {
            gameResultPanel.Hide(scheduleItemsPanel);
            expandedSchedule.ArrowImage = "/Assets/Images/ArrowDown.png";
            expandedSchedule = null;
        }

        #endregion

        #region Subscribe

        private void Subscribe_Tap(object sender, GestureEventArgs e)
        {
            GameSchedule schedule = sender.GetDataContext<GameSchedule>();
            if (schedule.Subscribed)
            {
                ReminderHelper.RemoveReminder(schedule.ID);
                SubscriptionDataContext.Current.RemoveSubscription(schedule.ID);
                schedule.Subscribed = false;
                toast.ShowMessage("成功取消预约。");
            }
            else
            {
                schedule.StartTime = schedule.StartTime;// DateTime.Now.AddSeconds(30);
                try
                {
                    var successful = ReminderHelper.AddReminder(schedule.ID, schedule.Category, schedule.Match, schedule.StartTime, string.Format("/Pages/LivePage.xaml?{0}={1}", NaviParam.SCHEDULE_ID, schedule.ID));
                    if (successful)
                    {
                        SubscriptionDataContext.Current.AddSubscription(schedule);
                        schedule.Subscribed = true;
                        toast.ShowMessage("成功添加预约。");
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

    }
}
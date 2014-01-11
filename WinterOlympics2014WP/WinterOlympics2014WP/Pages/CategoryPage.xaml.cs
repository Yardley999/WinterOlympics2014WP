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
using Microsoft.Phone.Net.NetworkInformation;
using System.IO;
using WinterOlympics2014WP.Utility;
using WinterOlympics2014WP.Models;

namespace WinterOlympics2014WP.Pages
{
    public partial class CategoryPage : PhoneApplicationPage
    {
        #region Property

        private bool busy;
        private bool dataLoaded;

        #endregion

        #region Lifecycle

        public CategoryPage()
        {
            InitializeComponent();
            scheduleListBox.ItemsSource = scheduleList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadSchedules();
        }

        #endregion

        #region Schedule List

        ObservableCollection<GameSchedule> scheduleList = new ObservableCollection<GameSchedule>();

        private void LoadSchedules()
        {
            if (busy)
            {
                return;
            }

            if (dataLoaded)
            {
                return;
            }

            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }

            try
            {
                String url = "http://115.28.21.97/api/server?cmd=getschedule&id=20256";
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
            dataLoaded = true;
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            WebResponse response = request.EndGetResponse(result);

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var list = JsonSerializer.Deserialize<GameScheduleList>(json);
                Dispatcher.BeginInvoke(() =>
                {
                    for (int i = 0; i < list.data.Length; i++)
                    {
                        scheduleList.Add(list.data[i]);
                    }
                });

                SaveNews(json);
            }
        }

        private async void SaveNews(string content)
        {
            await IsolatedStorageHelper.WriteToFile("news", "latest_news.txt", content);
        }

        #endregion

        private void Subscribe_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            GameSchedule schedule = sender.GetDataContext<GameSchedule>();
            schedule.StartTime = DateTime.Now.AddSeconds(30);
            ReminderHelper.AddReminder(schedule.ID, schedule.Category, schedule.Match, schedule.StartTime, "/Pages/LivePage.xaml");
        }

        private void ExpandSchedule_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}
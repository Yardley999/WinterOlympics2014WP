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
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Pages
{
    public partial class LivePage : PhoneApplicationPage
    {
        #region Property

        private string programID = string.Empty;

        #endregion

        #region Lifecycle

        public LivePage()
        {
            InitializeComponent();
            descriptionListBox.ItemsSource = descriptionList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            programID = NavigationContext.QueryString[NaviParam.PROGRAM_ID];
            LoadProgramData(programID);
        }

        #endregion

        #region Data

        ListDataLoader<Program> programLoader = new ListDataLoader<Program>();

        private void LoadProgramData(string id)
        {
            //TO-DO : remove test line
            id = "1000000106";

            if (programLoader.Loaded || programLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            programLoader.Load("getlivepage", "&id=" + id, true, Constants.PROGRAM_MODULE, string.Format(Constants.PROGRAM_FILE_NAME_FORMAT, id),
                program =>
                {
                    this.DataContext = program;

                    //TO-DO : check where to do this update
                    UpdateDescriptionList();

                    snow1.IsBusy = false;
                });
        }


        #endregion

        private void Video_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void Albumn_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //TO-DO : pass correct album id
            string naviString = string.Format("/Pages/AlbumPage.xaml?{0}={1}", NaviParam.ALBUM_ID, "4a303dc1070f25dd");
            NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
        }

        #region Description

        ObservableCollection<string> descriptionList = new ObservableCollection<string>();

        private void UpdateDescriptionList()
        {
            //TO-DO : do some real thing

            descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            //descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            //descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
            //descriptionList.Add("距离比赛结束只有不到5分钟，突然杀出一个程咬金，在关键时刻打入制胜一球，上演了惊天大逆转！");
        }

        #endregion
    }
}
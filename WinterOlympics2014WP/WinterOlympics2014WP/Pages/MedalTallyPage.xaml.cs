using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;
using Microsoft.Phone.Shell;
using System;

namespace WinterOlympics2014WP.Pages
{
    public partial class MedalTallyPage : PhoneApplicationPage
    {
        #region Property

        #endregion

        #region Lifecycle

        public MedalTallyPage()
        {
            InitializeComponent();
            BuildApplicationBar();
            medalListBox.ItemsSource = medalScoreList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadMedalTally();
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //    if (e.Uri.OriginalString != "app://external/")
        //    {
        //        HidePage();
        //    }
        //}

        #endregion

        #region Medal Tally

        ObservableCollection<MedalScore> medalScoreList = new ObservableCollection<MedalScore>();
        ListDataLoader<MedalScore> medalsLoader = new ListDataLoader<MedalScore>();

        private void LoadMedalTally()
        {
            if (medalsLoader.Loaded || medalsLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            medalsLoader.Load("getmedals", string.Empty, true, Constants.MEDAL_TALLY_MODULE, Constants.MEDAL_TALLY_FILE_NAME,
                list =>
                {
                    medalScoreList.Clear();
                    foreach (var item in list)
                    {
                        medalScoreList.Add(item);
                    }

                    scrollViewer.ScrollToVerticalOffset(0);
                    snow1.IsBusy = false;
                });
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarRefresh;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh
            appBarRefresh = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefresh.Text = "刷新";
            appBarRefresh.Click += appBarRefresh_Click;
            ApplicationBar.Buttons.Add(appBarRefresh);
        }

        void appBarRefresh_Click(object sender, System.EventArgs e)
        {
            medalsLoader.Loaded = false;
            LoadMedalTally();
        }

        #endregion

        //#region Page Navigation Transition

        //FadeAnimation fadeAnimation = new FadeAnimation();
        //MoveAnimation moveAnimation = new MoveAnimation();

        //private void ShowPage()
        //{
        //    this.contentPanel.UpdateLayout();
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 90, 0, 0, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
        //}

        //private void HidePage()
        //{
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 0, 0, 90, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 1d, 0d, Constants.NAVIGATION_DURATION, null);
        //}

        //#endregion
    }
}
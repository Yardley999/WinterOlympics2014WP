using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace WinterOlympics2014WP.Controls
{
    public partial class EPGList : UserControl
    {
        #region Property

        public Page HostingPage { get; set; }

        private QuickSelector quickSelector = null;
        public QuickSelector QuickSelector
        {
            get
            {
                return quickSelector;
            }
            set
            {
                quickSelector = value;
                if (quickSelector != null)
                {
                    InitQuickSelector();
                }
            }
        }

        #endregion

        #region Lifecycle

        public EPGList()
        {
            InitializeComponent();
        }

        #endregion

        #region EPG List

        ObservableCollection<int> epgList = new ObservableCollection<int>();

        public void PopulateData(DateTime date)
        {
            epgList.Clear();

            epgList.Add(0);
            epgList.Add(1);
            epgList.Add(2);
            epgList.Add(3);
            epgList.Add(4);
            epgList.Add(5);
            epgList.Add(6);
            epgList.Add(7);
            epgList.Add(8);
            epgList.Add(9);
            epgListBox.ItemsSource = epgList;
        }

        private void EpgItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage!=null)
            {
                HostingPage.NavigationService.Navigate(new Uri("/Pages/LivePage.xaml", UriKind.Relative));
            }
        }

        #endregion

        #region QuickSelector

        Dictionary<string, bool> hoursOfDay = new Dictionary<string, bool>();
        List<string> hoursList = new List<string>();

        private void InitQuickSelector()
        {
            hoursOfDay.Add("00:00", true);
            hoursOfDay.Add("01:00", false);
            hoursOfDay.Add("02:00", true);
            hoursOfDay.Add("03:00", true);
            hoursOfDay.Add("04:00", false);
            hoursOfDay.Add("05:00", false);
            hoursOfDay.Add("06:00", false);
            hoursOfDay.Add("07:00", false);
            hoursOfDay.Add("08:00", false);
            hoursOfDay.Add("09:00", false);
            hoursOfDay.Add("10:00", false);
            hoursOfDay.Add("11:00", false);
            hoursOfDay.Add("12:00", false);
            hoursOfDay.Add("13:00", false);
            hoursOfDay.Add("14:00", false);
            hoursOfDay.Add("15:00", false);
            hoursOfDay.Add("16:00", false);
            hoursOfDay.Add("17:00", false);
            hoursOfDay.Add("18:00", false);
            hoursOfDay.Add("19:00", true);
            hoursOfDay.Add("20:00", true);
            hoursOfDay.Add("21:00", true);
            hoursOfDay.Add("22:00", true);
            hoursOfDay.Add("23:00", true);
            quickSelector.SetItems(hoursOfDay);

            hoursList = hoursOfDay.Keys.ToList();

            quickSelector.SelectionChanged += QuickSelector_SelectionChanged;
        }

        private void QuickSelector_SelectionChanged(object sender, int selectedIndex)
        {
            epgListBox.ScrollIntoView(6);
        }

        #endregion

    }
}

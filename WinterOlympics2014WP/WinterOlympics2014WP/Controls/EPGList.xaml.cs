using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using WinterOlympics2014WP.Utility;
using WinterOlympics2014WP.Models;

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
            epgListBox.ItemsSource = epgList;
        }

        #endregion

        #region EPG List

        ObservableCollection<EPG> epgList = new ObservableCollection<EPG>();
        ListDataLoader<EPG> epgLoader = new ListDataLoader<EPG>();

        public void LoadEpg(DateTime date)
        {
            if (epgLoader.Loaded || epgLoader.Busy)
            {
                return;
            }

            //TO-DO : get today instead of test date
            //TO-DO : set token and sign 
            string today = date.ToString("yyyy-MM-dd");
            string param = "&date=" + today + "&token=&sign=&t=";

            epgLoader.Load("getepg", param, true, Constants.EPG_MODULE, string.Format(Constants.EPG_FILE_NAME_FORMTAT, today),
                list =>
                {
                    if (list != null)
                    {
                        epgList.Clear();
                        List<DateTime> validHours = new List<DateTime>();

                        foreach (var item in list)
                        {
                            epgList.Add(item);
                            validHours.Add(item.Start);
                        }

                        SetQuickSelectorValidItems(validHours);
                    }
                });
        }

        private void EpgItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage != null)
            {
                HostingPage.NavigationService.Navigate(new Uri("/Pages/LivePage.xaml", UriKind.Relative));
            }
        }

        #endregion

        #region QuickSelector

        Dictionary<DateTime, bool> hoursOfDay = new Dictionary<DateTime, bool>();

        private void InitQuickSelector()
        {
            DateTime dt = DateTime.Today;
            for (int i = 0; i < 24; i++)
            {
                hoursOfDay.Add(dt, false);
                dt = dt.AddHours(1);
            }

            //initial values, all are invalid items
            quickSelector.SetItems(hoursOfDay);
            quickSelector.SelectionChanged += QuickSelector_SelectionChanged;
        }

        private void SetQuickSelectorValidItems(IEnumerable<DateTime> validItems)
        {
            var keyList = hoursOfDay.Keys.ToList();
            foreach (var key in keyList)
            {
                if (validItems.Any(x=>x.Hour == key.Hour))
                {
                    hoursOfDay[key] = true;
                }
            }

            //update again
            quickSelector.SetItems(hoursOfDay);
        }

        private void QuickSelector_SelectionChanged(object sender, DateTime selectedDateTime)
        {
            epgListBox.ScrollIntoView(epgList.FirstOrDefault(x=>x.Start.Hour == selectedDateTime.Hour));
        }

        #endregion

    }
}

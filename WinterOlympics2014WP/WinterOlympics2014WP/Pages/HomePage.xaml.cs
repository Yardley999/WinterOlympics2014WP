﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WinterOlympics2014WP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        public HomePage()
        {
            InitializeComponent();
            InitQuickSelector();
        }

        

        #region Quick Selector

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
        }

        private void QuickSelector_SelectionChanged(object sender, int selectedIndex)
        {
            MessageBox.Show(hoursList[selectedIndex]);
        }

        #endregion

    }
}
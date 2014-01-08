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
    public partial class MedalTallyPage : PhoneApplicationPage
    {
        public MedalTallyPage()
        {
            InitializeComponent();
            LoadMedalTally();
        }

        #region Medal Tally

        ObservableCollection<int> newsList = new ObservableCollection<int>();

        private void LoadMedalTally()
        {
            newsList.Clear();

            newsList.Add(0);
            newsList.Add(1);
            newsList.Add(2);
            newsList.Add(3);
            newsList.Add(4);
            newsList.Add(5);
            newsList.Add(6);
            newsList.Add(7);
            newsList.Add(8);
            newsList.Add(9);

            medalListBox.ItemsSource = newsList;
        }


        #endregion
    }
}
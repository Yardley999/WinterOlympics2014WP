using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinterOlympics2014WP.Utility;
using WinterOlympics2014WP.Models;

namespace WinterOlympics2014WP.Controls
{
    public partial class BannerControl : UserControl
    {
        public Action DismissAction = null;

        public BannerControl()
        {
            InitializeComponent();
        }

        private void dismissButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (DismissAction!=null)
            {
                DismissAction();
            }
        }


    }
}

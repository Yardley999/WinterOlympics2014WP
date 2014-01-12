using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinterOlympics2014WP.Animations;

namespace WinterOlympics2014WP.Controls
{
    public partial class Snow : UserControl
    {
        private bool isBusy = false;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                if (isBusy)
                {
                    StartSnowing();
                }
                else
                {
                    StopSnowing();
                }
            }
        }

        public Snow()
        {
            InitializeComponent();
        }

        private void StartSnowing()
        {
            Storyboard1.Begin();
            FadeAnimation.Fade(this, 0, 1d, TimeSpan.FromMilliseconds(300), null);
        }

        private void StopSnowing()
        {
            FadeAnimation.Fade(this, 1d, 0, TimeSpan.FromMilliseconds(300),
                fe =>
                {
                    Storyboard1.Pause();
                });
        }

    }
}

﻿using System;
using System.Windows.Controls;
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

        private bool snowing = false;
        private void StartSnowing()
        {
            if (!snowing)
            {
                snowing = true;
                Storyboard1.Begin();
                FadeAnimation.Fade(this, 0, 1d, TimeSpan.FromMilliseconds(300), null);
            }
        }

        private void StopSnowing()
        {
            if (snowing)
            {
                snowing = false;
                FadeAnimation.Fade(this, 1d, 0, TimeSpan.FromMilliseconds(300),
                fe =>
                {
                    Storyboard1.Stop();
                });
            }
        }

    }
}

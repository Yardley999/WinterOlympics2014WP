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
    public partial class AlbumPage : PhoneApplicationPage
    {
        private int imageCount = 0;
        private int currentIndex = 0;
        ObservableCollection<int> imageList = new ObservableCollection<int>();
        private bool bottomPanelShown = true;

        #region Lifecycle

        public AlbumPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetAlbumData();
        }

        #endregion

        #region Data


        private void SetAlbumData()
        {
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);
            imageList.Add(1);

            imageCount = imageList.Count;
            currentIndex = 0;
            UpdateCurrentIndex();
        }

        #endregion

        int previousPanoramaIndex = 0;

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool swipeToLeft = false;
            if (panorama.SelectedIndex == 0)
            {
                if (previousPanoramaIndex == 1)
                {
                    swipeToLeft = false;
                }
                else if (previousPanoramaIndex == 2)
                {
                    swipeToLeft = true;
                }
            }
            else if (panorama.SelectedIndex == 1)
            {
                if (previousPanoramaIndex == 2)
                {
                    swipeToLeft = false;
                }
                else if (previousPanoramaIndex == 0)
                {
                    swipeToLeft = true;
                }
            }
            else if (panorama.SelectedIndex == 2)
            {
                if (previousPanoramaIndex == 0)
                {
                    swipeToLeft = false;
                }
                else if (previousPanoramaIndex == 1)
                {
                    swipeToLeft = true;
                }
            }

            previousPanoramaIndex = panorama.SelectedIndex;

            if (swipeToLeft)
            {
                currentIndex++;
                if (currentIndex >= imageCount)
                {
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = imageCount - 1;
                }
            }

            UpdateCurrentIndex();
        }

        private void UpdateCurrentIndex()
        {
            indexTextBlock.Text = (currentIndex + 1).ToString() + "/" + imageCount.ToString();
        }

        private void Panorama_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bottomPanelShown = !bottomPanelShown;
            if (bottomPanelShown)
            {
                VisualStateManager.GoToState(this, "BottomPanelShown", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "BottomPanelHidden", true);
            }
        }
    }
}
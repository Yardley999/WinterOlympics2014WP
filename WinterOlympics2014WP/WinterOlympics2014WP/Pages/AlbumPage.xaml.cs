using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System;
using WinterOlympics2014WP.Models;
using System.Collections.Generic;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Pages
{
    public partial class AlbumPage : PhoneApplicationPage
    {
        #region Property

        private int imageCount = 0;
        private int currentIndex = 0;
        private bool bottomPanelShown = true;
        Image imageCenter, imageLeft, imageRight;
        private string albumID = string.Empty;

        #endregion

        #region Lifecycle

        public AlbumPage()
        {
            InitializeComponent();
            imageCenter = image1;
            imageLeft = image3;
            imageRight = image2;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            albumID = NavigationContext.QueryString[NaviParam.ALBUM_ID];
            FitOrientation();
            LoadAlbumData(albumID);
        }

        #endregion

        #region Data

        DataLoader<Album> albumloader = new DataLoader<Album>();
        ObservableCollection<AlbumItem> albumItems = new ObservableCollection<AlbumItem>();

        private void LoadAlbumData(string id)
        {
            if (albumloader.Loaded || albumloader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            albumloader.Load("getalbum", "&id=" + id,true, Constants.ALBUM_MODULE, string.Format(Constants.ALBUM_FILE_NAME_FORMAT, id),
                album =>
                {
                    albumItems.Clear();
                    foreach (var item in album.Items)
                    {
                        //TO-DO : remove following test line
                        item.Image = "http://images.ccoo.cn/bbs/2010224/201022423410247.jpg";
                        albumItems.Add(item);
                    }
                    imageCount = albumItems.Count;
                    currentIndex = 0;
                    UpdateCurrentIndex();
                    snow1.IsBusy = false;
                });
        }

        #endregion

        #region Selection

        int previousPanoramaIndex = 0;

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool swipeToLeft = false;
            if (panorama.SelectedIndex == 0)
            {
                imageCenter = image1;
                imageLeft = image3;
                imageRight = image2;

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
                imageCenter = image2;
                imageLeft = image1;
                imageRight = image3;

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
                imageCenter = image3;
                imageLeft = image2;
                imageRight = image1;

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
            descriptionTextBlock.Text = albumItems[currentIndex].Title;

            int indexForLeft = currentIndex == 0 ? (imageCount - 1) : (currentIndex - 1);
            int indexForRight = currentIndex == (imageCount - 1) ? 0 : (currentIndex + 1);

            imageCenter.Source = new BitmapImage(new Uri(albumItems[currentIndex].Image, UriKind.RelativeOrAbsolute));
            imageLeft.Source = new BitmapImage(new Uri(albumItems[indexForLeft].Image, UriKind.RelativeOrAbsolute));
            imageRight.Source = new BitmapImage(new Uri(albumItems[indexForRight].Image, UriKind.RelativeOrAbsolute));
            //imageCenter.Source = await ImageCacheDataContext.Current.GetImage(albumItems[currentIndex].Image, true);
            //imageLeft.Source = await ImageCacheDataContext.Current.GetImage(albumItems[indexForLeft].Image, true);
            //imageRight.Source = await ImageCacheDataContext.Current.GetImage(albumItems[indexForRight].Image, true);
        }

        #endregion

        #region Tap

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

        #endregion

        #region Orientatin

        Thickness landscape_margin = new Thickness(58, 0, 0, 0);
        Thickness portrait_margin = new Thickness(61, 0, 0, 0);
        double landscape_scale = 1.16d;
        double portrait_scale = 1.29d;
        double landscape_grid_width = 690;
        double landscape_grid_height = 415;
        double portrait_grid_width = 372;
        double portrait_grid_height = 620;

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            FitOrientation();
        }

        private void FitOrientation()
        {
            if (this.Orientation == PageOrientation.PortraitUp || this.Orientation == PageOrientation.PortraitDown)
            {
                panorama.Margin = portrait_margin;
                panoramaTransform.ScaleX = portrait_scale;
                panoramaTransform.ScaleY = portrait_scale;
                grid1.Width = grid2.Width = grid3.Width = portrait_grid_width;
                grid1.Height = grid2.Height = grid3.Height = portrait_grid_height;
            }
            else if (this.Orientation == PageOrientation.LandscapeLeft || this.Orientation == PageOrientation.LandscapeRight)
            {
                panorama.Margin = landscape_margin;
                panoramaTransform.ScaleX = landscape_scale;
                panoramaTransform.ScaleY = landscape_scale;
                grid1.Width = grid2.Width = grid3.Width = landscape_grid_width;
                grid1.Height = grid2.Height = grid3.Height = landscape_grid_height;
            }
        }

        #endregion

    }
}
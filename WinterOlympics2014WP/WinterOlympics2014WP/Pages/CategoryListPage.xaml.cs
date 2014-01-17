using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinterOlympics2014WP.Models;
using WinterOlympics2014WP.Utility;
using WinterOlympics2014WP.Animations;

namespace WinterOlympics2014WP.Pages
{
    public partial class CategoryListPage : PhoneApplicationPage
    {
        #region Lifecycle

        public CategoryListPage()
        {
            InitializeComponent();
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

        #region Category List

        ListDataLoader<Category> loader = new ListDataLoader<Category>();
        List<Category> categories = new List<Category>();

        private void LoadCategories()
        {
            if (loader.Loaded || loader.Busy)
            {
                return;
            }

            loader.Load("getcate", string.Empty, true, Constants.CATEGORY_MODULE, Constants.CATEGORY_LIST_FILE_NAME,
                list =>
                {
                    categories = list;
                    //ShowPage();
                });
        }

        private void PopulateCategoryListBox()
        {

        }

        #endregion

        //#region Page Navigation Transition

        //FadeAnimation fadeAnimation = new FadeAnimation();
        //MoveAnimation moveAnimation = new MoveAnimation();

        //private void ShowPage()
        //{
        //    contentPanel.UpdateLayout();
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 90, 0, 0, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
        //}

        //private void HidePage()
        //{
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 0, 0, 90, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 1d, 0d, Constants.NAVIGATION_DURATION, null);
        //}

        //#endregion

        private void Item_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string strUri = string.Format("/Pages/CategoryPage.xaml?{0}={1}", NaviParam.CATEGORY_ID, "20256");
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
        }

    }
}
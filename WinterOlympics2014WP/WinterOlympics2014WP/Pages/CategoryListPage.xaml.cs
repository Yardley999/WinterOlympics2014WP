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

namespace WinterOlympics2014WP.Pages
{
    public partial class CategoryListPage : PhoneApplicationPage
    {
        public CategoryListPage()
        {
            InitializeComponent();
        }

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
                });
        }

        private void PopulateCategoryListBox()
        {

        }

        #endregion

        private void Item_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string strUri = string.Format("/Pages/CategoryPage.xaml?{0}={1}", NaviParam.CATEGORY_ID, "20256");
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
        }

    }
}
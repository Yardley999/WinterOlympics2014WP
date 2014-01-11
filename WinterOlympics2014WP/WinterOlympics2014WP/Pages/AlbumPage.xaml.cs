using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System;
using WinterOlympics2014WP.Models;
using System.Collections.Generic;

namespace WinterOlympics2014WP.Pages
{
    public partial class AlbumPage : PhoneApplicationPage
    {
        #region Property

        private int imageCount = 0;
        private int currentIndex = 0;
        private bool bottomPanelShown = true;
        private bool dataLoaded = false;
        Image imageCenter, imageLeft, imageRight;

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
            FitOrientation();
            if (!dataLoaded)
            {
                LoadAlbumData();
                dataLoaded = true;
            }
        }

        #endregion

        #region Data

        ObservableCollection<AlbumItem> albumItems = new ObservableCollection<AlbumItem>();

        private void LoadAlbumData()
        {
            albumItems.Add(new AlbumItem("http://sports.shangdu.com/uploadfile/2010_3/5/849017808.jpg", "2014年索契冬季奥运会（英语：2014 Winter Olympics）暨第22届冬季奥林匹克运动会，简称“索契奥运会”，将于2014年2月7日至2月23日在俄罗斯联邦索契市举行"));
            albumItems.Add(new AlbumItem("http://gb.cri.cn/mmsource/images/2010/02/15/wb100215055.jpg", "索契奥运会设15个大项，98小项。2014年索契冬季奥运会是俄罗斯历史上第一次举办冬季奥运会。"));
            albumItems.Add(new AlbumItem("http://img.gmw.cn/imgnews/attachement/jpg/site2/20060216/xin_190203161009468106262.jpg", "2013年9月28日2014年索契冬季奥运会圣火采集彩排仪式在希腊古奥林匹亚举行，组织者成功地采集到冬奥会圣火火种。"));
            albumItems.Add(new AlbumItem("http://i1.sinaimg.cn/ty/o/p/2010-02-16/U347P6T12D4841650F44DT20100216121414.jpg", "圣火火种将保留到29日正式的圣火采集仪式。"));
            albumItems.Add(new AlbumItem("http://www.qh.xinhuanet.com/plateausports/2010-03/15/xin_16303071515526251438131.jpg", "2013年10月20日，奥运圣火搭乘核动力破冰船首次抵达北极，在零下15摄氏度下，圣火燃烧得仍然“很棒”。"));
            albumItems.Add(new AlbumItem("http://i3.sinaimg.cn/ty/o/2010-02-14/U1532P6T12D4839366F44DT20100214141112.jpg", "2013年11月23日，2014年索契冬奥会火炬从布里亚特共和国首府乌兰乌德抵达伊尔库茨克州，并且在贝加尔湖湖底成功传递。"));
            albumItems.Add(new AlbumItem("http://www.hntopdx.com/backdoor/UploadFiles/201092519573392.jpg", "俄罗斯当地时间2007年7月4日下午，在危地马拉首都危地马拉城举行的国际奥委会第119次全会确定2014年冬奥会举办地归属，俄罗斯的索契战胜对手韩国的平昌，获得2014年冬季奥林匹克运动会的举办权，两个城市的票差为4票。"));
            albumItems.Add(new AlbumItem("http://i1.sinaimg.cn/ty/o/p/2010-02-16/U1612P6T12D4841610F30DT20100216115902.jpg", "本次冬奥会主办权的争夺异常激烈，参选各国都派出了众多重量级的人物。"));
            albumItems.Add(new AlbumItem("http://photocdn.sohu.com/20110805/Img315548136.jpg", "中国著名花样滑冰运动员陈露获邀担任索契的申奥大使，获得四枚奥运会金牌的俄罗斯游泳选手波波夫则到第119次全会现场为索契助阵。"));
            albumItems.Add(new AlbumItem("http://photocdn.sohu.com/20060215/Img241835346.jpg", "2014年索契冬季奥运会圣火采集彩排仪式2013年9月28日在希腊古奥林匹亚举行，组织者成功地采集到冬奥会圣火火种。"));
            albumItems.Add(new AlbumItem("http://images.china.cn/news/attachement/jpg/site3/20100224/5225894353500390540.jpg", "当地时间2013年11月7日，哈萨克斯坦，联盟号太空船成功升空。"));
            albumItems.Add(new AlbumItem("http://news.xinhuanet.com/sports/2013-02/09/medium-a3fc69d3-1538-488d-936c-32c884a4b37d1n.jpg", "索契（俄语：Сочи，拉丁字母转写：Sochi），俄罗斯联邦克拉斯诺达尔边疆区与格鲁吉亚接界处、黑海沿岸，宽40至60公里，东西长145公里，是俄罗斯最狭长的城市，也是俄罗斯最受欢迎的度假胜地，其道路和楼房均依山势而建，在这里乘车观光，最能体会峰回路转的视觉愉悦。"));
            albumItems.Add(new AlbumItem("http://images.ccoo.cn/bbs/2010224/201022423410247.jpg", "得益于依山傍海的独特地理优势，索契成为地球最北端的亚热带气候区。"));
            albumItems.Add(new AlbumItem("http://img4.cache.netease.com/photo/0005/2010-02-25/60CILN432AGA0005.jpg", "大高加索山脉阻挡了北方的冷空气，黑海又像巨大的“暖水袋”一样散发热量，使索契终年温暖湿润，四季如春，半年时间都可以下海游泳。"));
            albumItems.Add(new AlbumItem("http://photocdn.sohu.com/20100210/Img270190981.jpg", "索契是俄罗斯最大的疗养地。"));
            albumItems.Add(new AlbumItem("http://bddsb.bandao.cn/data/20100221/4648333535343538/images/18.jpg", "索契冬奥会吉祥物26日经俄罗斯全国投票决出，雪豹、白熊和兔子分列三甲最终胜出。"));

            imageCount = albumItems.Count;
            currentIndex = 0;
            UpdateCurrentIndex();
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
using System;
using System.Net;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WinterOlympics2014WP.Models;
using Microsoft.Phone.Net.NetworkInformation;
using System.IO;
using WinterOlympics2014WP.Utility;

namespace WinterOlympics2014WP.Pages
{
    public partial class MedalTallyPage : PhoneApplicationPage
    {
        #region Property

        private bool busy = false;

        #endregion

        #region Lifecycle

        public MedalTallyPage()
        {
            InitializeComponent();
            medalListBox.ItemsSource = medalScoreList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadMedalTally();
        }

        #endregion

        #region Medal Tally

        bool MedalScoreLoaded = false;
        ObservableCollection<MedalScore> medalScoreList = new ObservableCollection<MedalScore>();

        private void LoadMedalTally()
        {
            if (busy)
            {
                return;
            }

            if (MedalScoreLoaded)
            {
                return;
            }

            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }

            try
            {
                String url = Constants.DOMAIN + "/api/server?cmd=getmedals";
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
                request.Method = "GET";
                request.BeginGetResponse(GetMedalScoreList_Callback, request);
                busy = true;
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
        }

        private async void GetMedalScoreList_Callback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                WebResponse response = request.EndGetResponse(result);

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    var list = JsonSerializer.Deserialize<MedalScoreList>(json);
                    Dispatcher.BeginInvoke(() =>
                    {
                        for (int i = 0; i < list.data.Length; i++)
                        {
                            medalScoreList.Add(list.data[i]);
                        }
                    });

                    await IsolatedStorageHelper.WriteToFile(Constants.MEDAL_TALLY_MODULE, Constants.MEDAL_TALLY_FILE_NAME, json);
                }
                MedalScoreLoaded = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                busy = false;
            }
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterOlympics2014WP
{
    public partial class App
    {
        IsolatedStorageSettings _Settings = IsolatedStorageSettings.ApplicationSettings;

        private const string KEY_WIFI_IMAGE = "wifi_image";

        private bool _DownloadImageOnlyOnWifi = false;
        public bool DownloadImageOnlyOnWifi
        {
            get
            {
                return _DownloadImageOnlyOnWifi;
            }
            set
            {
                if (_DownloadImageOnlyOnWifi != value)
                {
                    _DownloadImageOnlyOnWifi = value;
                    UpdateSetting(KEY_WIFI_IMAGE, value);
                }
            }
        }

        private void LoadSettings()
        {
            ////wifi image
            //if (_Settings.Contains(KEY_WIFI_IMAGE))
            //{
            //    _DownloadImageOnlyOnWifi = (bool)_Settings[KEY_WIFI_IMAGE];
            //}
            //else
            //{
            //    _Settings.Add(KEY_WIFI_IMAGE, false);
            //}

            //_Settings.Save();
        }

        private void UpdateSetting(string key, object value)
        {
            _Settings[key] = value;
            _Settings.Save();
        }
    }
}

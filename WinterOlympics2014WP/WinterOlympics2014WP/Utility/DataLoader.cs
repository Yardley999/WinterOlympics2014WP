using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;

namespace WinterOlympics2014WP.Utility
{
    public class DataLoader<T> where T : class
    {
        public bool Busy = false;
        public bool Loaded = false;

        private Action<T> onCallback;
        string moduleName = string.Empty;
        string fileName = string.Empty;
        private bool toSaveData = false;

        public void Load(string cmd, Action<T> callback)
        {
            this.Load(cmd, string.Empty, callback, false, string.Empty, string.Empty);
        }

        public void Load(string cmd, Action<T> callback, string module, string file)
        {
            this.Load(cmd, string.Empty, callback, true, module, file);
        }

        public void Load(string cmd, string param, Action<T> callback, bool saveData, string module, string file)
        {
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }

            if (saveData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
            {
                return;
            }

            onCallback = callback;
            toSaveData = saveData;
            moduleName = module;
            fileName = file;

            try
            {
                String url = Constants.DOMAIN + "/api/server?cmd=" + cmd.Trim() + param.Trim();
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
                request.Method = "GET";
                request.BeginGetResponse(GetMedalScoreList_Callback, request);

                Loaded = false;
                Busy = true;
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
                    JsonObjectWrapper<T> wrapper = JsonSerializer.Deserialize<JsonObjectWrapper<T>>(json);
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        onCallback(wrapper.data);
                    });

                    if (toSaveData)
                    {
                        await IsolatedStorageHelper.WriteToFile(moduleName, fileName, json);
                    }
                }
                Loaded = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                Busy = false;
            }
        }
    }

    [DataContract]
    public class JsonObjectWrapper<T>
    {
        [DataMember]
        public T data { get; set; }
    }

    public class ListDataLoader<T>
    {
        public bool Busy = false;
        public bool Loaded = false;

        private Action<List<T>> onCallback;
        string moduleName = string.Empty;
        string fileName = string.Empty;
        private bool toSaveData = false;

        public void Load(string cmd, Action<List<T>> callback)
        {
            this.Load(cmd, string.Empty, callback, false, string.Empty, string.Empty);
        }

        public void Load(string cmd, Action<List<T>> callback, string module, string file)
        {
            this.Load(cmd, string.Empty, callback, true, module, file);
        }

        public void Load(string cmd, string param, Action<List<T>> callback, bool saveData, string module, string file)
        {
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }

            if (saveData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
            {
                return;
            }

            onCallback = callback;
            moduleName = module;
            fileName = file;
            toSaveData = saveData;

            try
            {
                String url = Constants.DOMAIN + "/api/server?cmd=" + cmd.Trim() + param.Trim();
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
                request.Method = "GET";
                request.BeginGetResponse(GetMedalScoreList_Callback, request);

                Loaded = false;
                Busy = true;
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
                    JsonArrayWrapper<T> wrapper = JsonSerializer.Deserialize<JsonArrayWrapper<T>>(json);
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        List<T> list = new List<T>();
                        if (wrapper.data != null)
                        {
                            for (int i = 0; i < wrapper.data.Length; i++)
                            {
                                list.Add(wrapper.data[i]);
                            }
                        }
                        onCallback(list);
                    });

                    if (toSaveData)
                    {
                        await IsolatedStorageHelper.WriteToFile(moduleName, fileName, json);
                    }
                }
                Loaded = true;
            }
            catch (Exception e)
            {
            }
            finally
            {
                Busy = false;
            }
        }
    }

    [DataContract]
    public class JsonArrayWrapper<T>
    {
        [DataMember]
        public T[] data { get; set; }
    }
}

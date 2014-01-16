using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
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
        private bool toCacheData = false;

        public void LoadWithoutCaching(string cmd, Action<T> callback)
        {
            this.Load(cmd, string.Empty, false, string.Empty, string.Empty, callback);
        }

        /* don't even try to convert this method into an awaitable method, as the callback should be called twice: 
         * first when local cache is loaded,  second when the new data is downloaded. Async-callback approach in better solution
         * than awaitable method for such use case.
        */
        public async void Load(string cmd, string param, bool cacheData, string module, string file, Action<T> callback)
        {
            if (cacheData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
            {
                return;
            }

            //for callback
            onCallback = callback;
            toCacheData = cacheData;
            moduleName = module;
            fileName = file;

            //load cache
            if (cacheData)
            {
                try
                {
                    var cachedJson = await IsolatedStorageHelper.ReadFile(moduleName, fileName);
                    JsonObjectWrapper<T> wrapper = JsonSerializer.Deserialize<JsonObjectWrapper<T>>(cachedJson);
                    if (wrapper != null && wrapper.data != null)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            onCallback(wrapper.data);
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }

            //download new
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }
            try
            {
                String url = Constants.DOMAIN + "/api/server?cmd=" + cmd.Trim() + param.Trim();
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
                request.Method = "GET";
                request.BeginGetResponse(GetData_Callback, request);

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

        private async void GetData_Callback(IAsyncResult result)
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
                    if (wrapper != null && wrapper.data != null)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            onCallback(wrapper.data);
                        });
                    }

                    if (toCacheData)
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
        private bool toCacheData = false;

        public void Load(string cmd, Action<List<T>> callback)
        {
            this.Load(cmd, string.Empty, false, string.Empty, string.Empty, callback);
        }

        public async void Load(string cmd, string param, bool cacheData, string module, string file, Action<List<T>> callback)
        {
            if (cacheData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
            {
                return;
            }

            //for callback
            onCallback = callback;
            moduleName = module;
            fileName = file;
            toCacheData = cacheData;

            //load cache
            if (cacheData)
            {
                try
                {
                    var cachedJson = await IsolatedStorageHelper.ReadFile(moduleName, fileName);
                    JsonArrayWrapper<T> wrapper = JsonSerializer.Deserialize<JsonArrayWrapper<T>>(cachedJson);
                    if (wrapper != null && wrapper.data != null)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            List<T> list = new List<T>();
                            for (int i = 0; i < wrapper.data.Length; i++)
                            {
                                list.Add(wrapper.data[i]);
                            }
                            onCallback(list);
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }

            //download new
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }
            try
            {
                String url = Constants.DOMAIN + "/api/server?cmd=" + cmd.Trim() + param.Trim();
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
                request.Method = "GET";
                request.BeginGetResponse(GetData_Callback, request);

                Loaded = false;
                Busy = true;
            }
            catch (Exception e)
            {
            }
        }

        private async void GetData_Callback(IAsyncResult result)
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
                    if (wrapper != null && wrapper.data != null)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            List<T> list = new List<T>();
                            for (int i = 0; i < wrapper.data.Length; i++)
                            {
                                list.Add(wrapper.data[i]);
                            }
                            onCallback(list);
                        });
                    }

                    if (toCacheData)
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

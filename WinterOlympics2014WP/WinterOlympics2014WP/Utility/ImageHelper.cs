using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Windows.Storage;

namespace WinterOlympics2014WP.Utility
{
    public class ImageHelper
    {
        static string folderName = string.Empty;
        static string fileName = string.Empty;
        Action onDownloaded = null;

        public void Download(string uri, string folder, string file, Action callback)
        {
            try
            {
                folderName = folder;
                fileName = file;
                onDownloaded = callback;

                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(uri));
                request.Method = "GET";
                request.BeginGetResponse(Download_Callback, request);
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
        }

        private async void Download_Callback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                WebResponse response = request.EndGetResponse(result);

                using (Stream stream = response.GetResponseStream())
                {
                    byte[] fileBytes = new byte[response.ContentLength];
                    await stream.ReadAsync(fileBytes, 0, (int)response.ContentLength);

                    // Get the local folder.
                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                    // Create a new folder
                    var dataFolder = await local.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

                    // Create a new file
                    var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                    // Write the data
                    using (var s = await file.OpenStreamForWriteAsync())
                    {
                        await s.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }

                    onDownloaded();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        public async Task<BitmapImage> ReadImage(string folder, string file)
        {
            try
            {
                // Get the local folder.
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                if (local != null)
                {
                    // Get the DataFolder folder.
                    var dataFolder = await local.GetFolderAsync(folder);

                    // Get the file.
                    var stream = await dataFolder.OpenStreamForReadAsync(file);

                    // Read the data.
                    var bi = new BitmapImage();
                    bi.SetSource(stream);
                    return bi;
                }
            }
            catch (Exception ex)
            {
            }
            
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace WinterOlympics2014WP.Utility
{
    // http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj681698(v=vs.105).aspx
    // http://blogs.msdn.com/b/wsdevsol/archive/2013/09/04/how-to-store-json-data-in-windows-phone-8.aspx

    public class IsolatedStorageHelper
    {
        public static async Task WriteToFile_msdn(string folderName, string fileName, string content)
        {
            // Get the text data 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(content);

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
        }

        public static async Task WriteToFile(string folderName, string fileName, string content)
        {
            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder
            var dataFolder = await local.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            // Create a new file
            var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // Write the data
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter writer = new DataWriter(stream))
                {
                    writer.WriteString(content);
                    await writer.StoreAsync();
                }
            }
        }

        public static async Task<string> ReadFile(string folderName, string fileName)
        {
            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                // Get the DataFolder folder.
                var dataFolder = await local.GetFolderAsync(folderName);

                if (dataFolder==null)
                {
                    return null;
                }
                // Get the file.
                var file = await dataFolder.OpenStreamForReadAsync(fileName);

                // Read the data.
                using (StreamReader streamReader = new StreamReader(file))
                {
                    return streamReader.ReadToEnd();
                }
            }
            return null;
        }

        public static async Task<string> ReadFile_blog(string folderName, string fileName, string content)
        {
            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                // Get the DataFolder folder.
                var dataFolder = await local.GetFolderAsync(folderName);

                // Get the file.
                var file = await dataFolder.GetFileAsync(fileName);

                // Read the data.
                using (IRandomAccessStream stream = await file.OpenReadAsync())
                {
                    // Read text stream     
                    using (DataReader reader = new DataReader(stream))
                    {
                        //get size                       
                        uint textLength = (uint)stream.Size;
                        await reader.LoadAsync(textLength);
                        // read it                    
                        return reader.ReadString(textLength);
                    }
                }
            }
            return null;
        }
    }

}

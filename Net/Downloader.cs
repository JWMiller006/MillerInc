using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace MillerInc.Net
{
    /// <summary>
    /// The Downloader class is used to download files from the internet with the given url
    /// </summary>
    public static class Downloader
    {
        /// <summary>
        /// Downloads a specific file from the server
        /// </summary>
        /// <param name="url">the url to the file</param>
        /// <param name="path">the path to save the file to</param>
        public static void ThreadedDownload(string url, string path)
        {
            Thread downloader = new(async () =>
            {
                using var client = new System.Net.Http.HttpClient(); // WebClient
                Stream stream;
                var fileName = path;
                fileName = fileName.Replace("%20", " ");
                var uri = new Uri(url);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                if (!Directory.Exists(Path.GetFullPath(path.Replace(Path.GetFileName(path), ""))))
                    Directory.CreateDirectory(Path.GetFullPath(path).Replace(Path.GetFileName(path), ""));
                stream = await client.GetStreamAsync(uri);
                FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                await stream.CopyToAsync(fs);
                fs.Flush();
                Thread.Sleep(100);
                fs.Close();
                stream.Close();
            });

            downloader.Start();
        }

        /// <summary>
        /// Downloads a specific file from the server
        /// </summary>
        /// <param name="url">the url to the file</param>
        /// <param name="path">the path to save the file to</param>
        public static async void DownloadFileAsync(string url, string path)
        {
            using var client = new System.Net.Http.HttpClient(); // WebClient
            Stream stream;
            var fileName = path;
            fileName = fileName.Replace("%20", " ");
            var uri = new Uri(url);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            if (!Directory.Exists(Path.GetFullPath(path.Replace(Path.GetFileName(path), ""))))
                Directory.CreateDirectory(Path.GetFullPath(path).Replace(Path.GetFileName(path), ""));
            stream = await client.GetStreamAsync(uri);
            FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            stream.CopyTo(fs);
            fs.Flush();
            Thread.Sleep(100);
                fs.Close();
                stream.Close();
        }
    }
}

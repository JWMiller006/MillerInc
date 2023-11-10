using MillerInc.Files;
using MillerInc.UI.OutputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using FileList = Google.Apis.Drive.v3.Data.FileList;
using Google.Apis.Download; 

namespace MillerInc.Net.GoogleDrive
{
    public class GoogleDriveControl
    {
        #region Initializers

        public GoogleDriveControl()
        {
            try
            {
                NewService();
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception e)
            {
                Output.WriteLine("Output.txt", e.ToString());
                Console.WriteLine(e.ToString());
            }
        }

        public GoogleDriveControl(string keyFile)
        {
            KeyFile = keyFile;
            NewService();
        }

        public GoogleDriveControl(GoogleCredential credentials)
        {
            Credentials = credentials;
            NewService(Credentials);
        }

        #endregion

        #region Fields (With basic input output modifiers)



        /// <summary>
        /// The stored credentials for the class
        /// </summary>
        internal GoogleCredential Credentials { get; set; }

        /// <summary>
        /// Sets the credentials and creates a new service
        /// </summary>
        /// <param name="credential"></param>
        public void SetCredentials(GoogleCredential credential)
        {
            Credentials = credential;
            NewService(Credentials);
        }



        /// <summary>
        /// The File from which the authentication is derived
        /// </summary>
        internal string KeyFile { get; set; } = "client_secret.json";



        /// <summary>
        /// The DriveServ with autonomously created credentials
        /// </summary>
        public DriveService DriveServ { get; set; }


        #region Base Class Types

        public string ServiceName { get; set; }

        public AboutResource About { get; set; }

        public string APIKey { get; set; }

        public string AppName { get; set; }

        public DateTime LastAccessed { get; set; }

        #endregion


        public List<GoogleFile> DirectFiles { get; set; } = new();


        #endregion

        #region Internal Methods 


        /// <summary>
        /// Sets up a new DriveServ from the file that has already been defined
        /// </summary>
        private void NewService()
        {
            Credentials = GoogleCredential.FromFile(KeyFile).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveServ = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credentials
            });
            ServiceName = DriveServ.Name;
            About = DriveServ.About;
            APIKey = DriveServ.ApiKey;
            AppName = DriveServ.ApplicationName;
            LastAccessed = DateTime.Now;
        }

        /// <summary>
        /// Sets up a new Drive service and sets new key
        /// </summary>
        /// <param name="keyFile">The path to the keyfile</param>
        public DriveService SetCredentials(string keyFile)
        {
            KeyFile = keyFile;
            DriveServ = NewService(keyFile);
            return DriveServ;
        }

        /// <summary>
        /// Sets up a new DriveServ from the credentials pre-defined
        /// </summary>
        /// <param name="credential"></param>
        private void NewService(GoogleCredential credential)
        {
            DriveServ = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            ServiceName = DriveServ.Name;
            About = DriveServ.About;
            APIKey = DriveServ.ApiKey;
            AppName = DriveServ.ApplicationName;
            LastAccessed = DateTime.Now;
        }


        /// <summary>
        /// Downloads the Google File input to the output path
        /// </summary>
        /// <param name="file">The Google File that is to be downloaded</param>
        /// <param name="outputPath">The output path that the file is dumped</param>
        public void DownloadFile(GoogleFile file, string outputPath)
        {
            if (CheckCompat(file))
            {
                var request = DriveServ.Files.Get(file.Id);
                var result = request.Execute();
                var stream = new MemoryStream();
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download Complete");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download Failed");
                                    break;
                                }
                        }
                    };
                request.Download(stream);
                FileControl.SaveStreamAsFile(stream, outputPath);
                var fileOut = result;
                //Files.FileControl.SaveStreamAsFile(fileOut, outputPath);
            }
            // Not complete, but it might work
            else
            {
                var request = DriveServ.Files.Get(file.Id);
                var result = request.Execute();
                var stream = new MemoryStream();
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download Complete");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download Failed");
                                    break;
                                }
                        }
                    };
                request.Download(stream);
                FileControl.SaveStreamAsFile(stream, outputPath);
            }
        }


        /// <summary>
        /// Class method that uses the pre-existing service to download the file to the output location
        /// </summary>
        /// <param name="fileName">The name or id of the file</param>
        /// <param name="outputPath">The output file path</param>
        public void DownloadFile(string fileName, string outputPath)
        {
            GoogleFile storedFile = new();
            var request2 = DriveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request2.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }

            if (CheckCompat(storedFile))
            {
                var request = DriveServ.Files.Get(storedFile.Id);
                var result = request.Execute();
                var stream = new MemoryStream();
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download Complete");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download Failed");
                                    break;
                                }
                        }
                    };
                request.Download(stream);
                FileControl.SaveStreamAsFile(stream, outputPath);
            }
            // Not complete, but it might work
            else
            {
                var request = DriveServ.Files.Get(storedFile.Id);
                var result = request.Execute();
                var stream = new MemoryStream();
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download Complete");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download Failed");
                                    break;
                                }
                        }
                    };
                request.Download(stream);
                FileControl.SaveStreamAsFile(stream, outputPath);
            }
        }


        /// <summary>
        /// Downloads the specified file to the output file
        /// </summary>
        /// <param name="fileName">The name or Id of the file that is being downloaded</param>
        /// <param name="outputPath">The path to the file that the output is copied to</param>
        /// <param name="keyFile">Path to the keyFile</param>
        public async void DownloadFileAsync(string fileName, string outputPath)
        {
            GoogleFile storedFile = new();
            var request2 = DriveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request2.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }

            if (CheckCompat(storedFile))
            {
                var request = DriveServ.Files.Get(storedFile.Id);
                var result = request.Execute();
                var stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download Complete");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download Failed");
                                    break;
                                }
                        }
                    };
                await request.DownloadAsync(stream);
                //await FileControl.SaveStreamAsFileAsync(stream, outputPath);
                stream.Close();
            }
            // Not complete, but it might work
            else
            {
                var request = DriveServ.Files.Get(storedFile.Id);
                var result = request.Execute();
                var stream = new MemoryStream();
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download Complete");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download Failed");
                                    break;
                                }
                        }
                    };
                await request.DownloadAsync(stream);
                await FileControl.SaveStreamAsFileAsync(stream, outputPath);
                stream.Close();

            }
        }






        #endregion

        #region Usable Methods


        /// <summary>
        /// Gets a list of the files in drive and stores them and metadata in the list
        /// </summary>
        public List<GoogleFile> GetFiles()
        {
            // Get Files 
            var request = DriveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request.Execute();
            foreach (var file in response.Files)
            {
                Console.WriteLine($"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
                Output.WriteLine("output.txt", $"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
            }
            DirectFiles = (List<GoogleFile>)response.Files;
            Output.WriteLine("output.txt", "Done fetching files");
            return DirectFiles;
        }




        /// <summary>
        /// Checks if the File able to be downloaded to the current device
        /// </summary>
        /// <param name="file">The file that is to be checked</param>
        /// <returns>The boolean of compatibility</returns>
        public static bool CheckCompat(GoogleFile file)
        {
            List<string> mimeTypes = new() { "text/plain", "application/zip", "application/pdf", "text/csv" };
            try
            {
                return Check.IsIn(file.MimeType, mimeTypes);
            }
            catch
            {
                return false;
            }
        }


        #endregion

        #region Static Methods 



        /// <summary>
        /// This is the Function that can be used async or sync just follow the DownloadHandler style
        /// </summary>
        /// <param name="fileName">Name of File to Search and download</param>
        /// <param name="outputPath">Path of File to download to</param>
        /// <param name="keyFile">Path to key file</param>
        /// <returns></returns>
        public static async Task DownloadFileAsync(string fileName, string outputPath, string keyFile)
        {
            GoogleFile storedFile = new();
            var driveServ = NewService(keyFile);
            var request2 = driveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request2.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }

            if (CheckCompat(storedFile))
            {
                var request = driveServ.Files.Get(storedFile.Id);
                var result = request.Execute();
                var stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download Complete");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download Failed");
                                    break;
                                }
                        }
                    };
                await request.DownloadAsync(stream);
                //await FileControl.SaveStreamAsFileAsync(stream, outputPath);
                stream.Close();
                return;
            }

            // Downloading non txt Files
            var mimeType = storedFile.MimeType;

            if (mimeType != "text/plain")
            {
                var stream2 = new System.IO.MemoryStream();
                await driveServ.Files.Get(storedFile.Id).DownloadAsync(stream2);
                var fileContent = stream2.ToArray();
                System.IO.File.WriteAllBytes(fileName, fileContent);
                stream2.Close();
                return;
            }

        }


        /// <summary>
        /// Downloads a file from the drive without creating a new object
        /// </summary>
        /// <param name="file">The GoogleFileStore specified for downloading</param>
        /// <param name="outputPath">The output location for the download data</param>
        /// <param name="keyFile">The path to the key file</param>
        public static void DownloadFile(GoogleFile file, string outputPath, string keyFile)
        {
            DriveService driveService = NewService(keyFile);
            var request = driveService.Files.Get(file.Id);
            var result = request.Execute();
            var stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            request.MediaDownloader.ProgressChanged +=
                progress =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download Complete");
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download Failed");
                                break;
                            }
                    }
                };
            request.Download(stream);
            //FileControl.SaveStreamAsFile(stream, outputPath);
            stream.Flush();
            stream.Close();
            // Downloading non txt Files
            var mimeType = file.MimeType;

            if (mimeType != "text/plain")
            {
                var stream2 = new System.IO.MemoryStream();
                driveService.Files.Get(file.Id).Download(stream);
                var fileContent = stream2.ToArray();
                System.IO.File.WriteAllBytes(outputPath, fileContent);
                stream2.Flush();
                stream2.Close();
            }
        }



        /// <summary>
        /// Downloads the specified file to the output file
        /// </summary>
        /// <param name="fileName">The name or Id of the file that is being downloaded</param>
        /// <param name="outputPath">The path to the file that the output is copied to</param>
        /// <param name="keyFile">Path to the keyFile</param>
        public static void DownloadFile(string fileName, string outputPath, string keyFile)
        {
            DriveService driveService = NewService(keyFile);
            GoogleFile storedFile = new();
            var request2 = driveService.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request2.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }

            var request = driveService.Files.Get(storedFile.Id);
            var result = request.Execute();
            //var stream = new MemoryStream();
            var stream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            request.MediaDownloader.ProgressChanged +=
                progress =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Output.WriteLine("output.txt", progress.BytesDownloaded.ToString());
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Output.WriteLine("output.txt", "Download Complete");
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Output.WriteLine("output.txt", "Download Failed");
                                break;
                            }
                    }
                };
            request.Download(stream);
            //FileControl.SaveStreamAsFile(stream, outputPath);
            stream.Flush();
            stream.Close();


            // Downloading non txt Files
            var mimeType = storedFile.MimeType;

            if (mimeType != "text/plain")
            {
                var stream2 = new System.IO.MemoryStream();
                driveService.Files.Get(storedFile.Id).Download(stream);
                var fileContent = stream2.ToArray();
                System.IO.File.WriteAllBytes(outputPath, fileContent);
                stream2.Close();
            }
            Output.WriteLine("output.txt", "Function Complete");
        }


        /// <summary>
        /// Deletes the file specified
        /// </summary>
        /// <param name="fileName">The Name or Id of the file</param>
        /// <param name="keyFile">The path to the keyFile</param>
        public static void DeleteFile(string fileName, string keyFile)
        {
            DriveService driveServ = NewService(keyFile);

            GoogleFile storedFile = new();
            var request2 = driveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request2.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }

            driveServ.Files.Delete(storedFile.Id).Execute();

        }


        /// <summary>
        /// Deletes the file specified
        /// </summary>
        /// <param name="fileName">The Name or Id of the file</param>
        /// <param name="keyFile">The path to the keyFile</param>
        public static async void DeleteFileAsync(string fileName, string keyFile)
        {
            DriveService driveServ = NewService(keyFile);

            GoogleFile storedFile = new();
            var request2 = driveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request2.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }

            await driveServ.Files.Delete(storedFile.Id).ExecuteAsync();

        }


        /// <summary>
        /// Returns the fileId of the file given
        /// </summary>
        /// <param name="fileName">Name or Id of the file on Google Drive</param>
        /// <param name="keyFile">Path to the Key File</param>
        /// <returns></returns>
        public static string GetFileId(string fileName, string keyFile)
        {
            DriveService driveServ = NewService(keyFile);
            GoogleFile storedFile = new();
            var request2 = driveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request2.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }
            return storedFile.Id;
        }


        /// <summary>
        /// Shares the file with the specified person
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="userEmail"></param>
        /// <param name="keyFile"></param>
        public static void ShareFileWithUser(string fileName, string userEmail, string keyFile, string permission = "owner")
        {
            DriveService driveService = NewService(keyFile);

            string fileId = GetFileId(fileName, keyFile);

            // Create a new permission for the user
            var newPermission = new Permission()
            {
                Type = "user",
                Role = permission,
                EmailAddress = userEmail
            };

            // Add the permission to the file
            driveService.Permissions.Create(newPermission, fileId).Execute();
        }


        /// <summary>
        /// Shares the file with the specified person
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="userEmail"></param>
        /// <param name="keyFile"></param>
        public async static Task ShareFileWithUserAsync(string fileName, string userEmail, string keyFile, string permission = "owner")
        {
            DriveService driveService = NewService(keyFile);

            string fileId = GetFileId(fileName, keyFile);

            // Create a new permission for the user
            var newPermission = new Permission()
            {
                Type = "user",
                Role = permission,
                EmailAddress = userEmail
            };

            // Add the permission to the file
            await driveService.Permissions.Create(newPermission, fileId).ExecuteAsync();
        }


        /// <summary>
        /// Uploads the file to google drive 
        /// </summary>
        /// <param name="fileName">The name of the uploaded file</param>
        /// <param name="filePath">The path to the uploaded file on the local drive</param>
        /// <param name="keyFile">The path to the keyFile </param>
        /// <param name="parentFolder">name of the parent folder</param>
        /// <param name="description">Description of the file to upload</param>
        public static void UploadFile(string fileName, string filePath, string keyFile, string parentFolder = null, string description = "")
        {
            DriveService driveServ = NewService(keyFile);
            FileStream stream = new(filePath, FileMode.Open);
            List<string> parentFolderList = new() { parentFolder };
            string extension = Path.GetExtension(stream.Name);
            string mimeType = GetMIMEFromExtension(extension);
            GoogleFile storedFile = new()
            {
                Name = fileName,
                Description = description,
                Parents = parentFolderList,
                MimeType = mimeType

            };
            FilesResource.CreateMediaUpload request;
            request = driveServ.Files.Create(storedFile, stream, mimeType);
            request.Fields = "id";
            request.Upload();
            stream.Close();
            ShareFileWithUser(fileName, "rec.data.store@gmail.com", keyFile);
        }


        /// <summary>
        /// Uploads the file to google drive 
        /// </summary>
        /// <param name="fileName">The name of the uploaded file</param>
        /// <param name="filePath">The path to the uploaded file on the local drive</param>
        /// <param name="keyFile">The path to the keyFile </param>
        /// <param name="parentFolder">name of the parent folder</param>
        /// <param name="description">Description of the file to upload</param>
        public async static void UploadFileAsync(string fileName, string filePath, string keyFile, string parentFolder = null, string description = "")
        {
            DriveService driveServ = NewService(keyFile);
            FileStream stream = new(filePath, FileMode.Open);
            List<string> parentFolderList = new() { parentFolder };
            string extension = Path.GetExtension(stream.Name);
            string mimeType = GetMIMEFromExtension(extension);
            GoogleFile storedFile = new()
            {
                Name = fileName,
                Description = description,
                Parents = parentFolderList,
                MimeType = mimeType

            };
            FilesResource.CreateMediaUpload request;
            request = driveServ.Files.Create(storedFile, stream, mimeType);
            request.Fields = "id";
            await request.UploadAsync();
            stream.Close();
            await ShareFileWithUserAsync(fileName, "rec.data.store@gmail.com", keyFile);
        }


        /// <summary>
        /// Gets the Google Drive MIME Type associated with the extension provided
        /// </summary>
        /// <param name="extension">The extension to compare with the MIME types</param>
        /// <returns>Google Drive MIME Type</returns>
        public static string GetMIMEFromExtension(string extension)
        {
            if (extension == ".txt")
            {
                return "text/plain";
            }
            else if (extension == ".zip")
            {
                return "application/zip";
            }
            else if (extension == ".docx")
            {
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else if (extension == ".odt")
            {
                return "application/vnd.oasis.opendocument.text";
            }
            else if (extension == ".rtf")
            {
                return "application/rtf";
            }
            else if (extension == ".epub")
            {
                return "application/epub+zip";
            }
            else if (extension == ".xlsx")
            {
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            else if (extension == ".ods")
            {
                return "application/x-vnd.oasis.opendocument.spreadsheet";
            }
            else if (extension == ".csv")
            {
                return "text/csv";
            }
            else if (extension == ".tsv")
            {
                return "text/tab-separated-values";
            }
            else if (extension == ".pptx")
            {
                return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            }
            else if (extension == ".odp")
            {
                return "application/vnd.oasis.opendocument.presentation";
            }
            else if (extension == ".jpg")
            {
                return "image/jpeg";
            }
            else if (extension == ".png")
            {
                return "image/png";
            }
            else if (extension == ".svg")
            {
                return "image/svg+xml";
            }
            else if (extension == ".json")
            {
                return "application/vnd.google-apps.script+json";
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// Gets a new driveservice from the specified file key
        /// </summary>
        /// <param name="keyFile">The path to the key file</param>
        /// <returns>A new DriveServ</returns>
        public static DriveService NewService(string keyFile)
        {
            var credentials = GoogleCredential.FromFile(keyFile).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });
            return driveService;
        }


        #endregion

        #region Overrides 


        /// <summary>
        /// Override ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = "";
            output += ServiceName + "\n";
            output += APIKey + "\n";
            output += About.ToString() + "\n";
            output += "Last Accessed: " + LastAccessed;
            return output;
        }


        #endregion

        #region Download Stages


        public static async void DownloadHandler(string file, string fileName, string keyFile)
        {
            await DownloadFileAsync(file, fileName, keyFile);
            Console.WriteLine("File Download Complete");
        }



        #endregion

    }
}

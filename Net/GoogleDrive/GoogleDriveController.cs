using Google.Apis.Drive.v3.Data;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using FileLists = Google.Apis.Drive.v3.Data.FileList;
using FileList = Google.Apis.Drive.v3.Data.FileList;
using Google.Apis.Download;
using MillerInc.UI.OutputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace MillerInc.Net.GoogleDrive
{
    /// <summary>
    /// Contains the controller methods for 
    /// accessing Google Drive
    /// </summary>
    public class GoogleDriveController
    {

        #region Initializers

        /// <summary>
        /// Creates a new Instance of GoogleControl. GoogleControl Initializes the Credentials 
        /// using the file listed in the project output folder (Make sure to include that) and 
        /// then sets up the read/write system
        /// </summary>
        public GoogleDriveController()
        {
            Credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveServ = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential
            });
            Console.WriteLine("Starting New Thread");
            Output.WriteLine("Output.txt", "Starting New Asyncronous Search For Files");
            Thread Files = new(new ThreadStart(GetFilesAsync));
            Files.Start();
        }

        #endregion

        #region Class Variables (Private)

        /// <summary>
        /// Must Include the stated file in the output folder
        /// </summary>
        private const string ServPath = "client_secret.json";

        private GoogleCredential Credential { get; set; }


        private DriveService DriveServ { get; set; }


        //private Output OutCon { get; set; } = new Output("Output.txt");


        private List<string> DownloadedFiles { get; set; } = new List<string>();

        private List<GoogleFile> Files { get; set; } = new List<GoogleFile>();


        #endregion

        #region Class Methods, Non-Static

        /// <summary>
        /// Gets the Files that were retrieved with the GetFiles();
        /// Must Confirm the Action 
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="AccessViolationException"></exception>
        public List<GoogleFile> FILES(bool conf)
        {
            if (conf)
            {
                return Files;
            }
            else
            {
                throw new Exception("Error: Restricted Access");
                throw new AccessViolationException();
            }
        }


        /// <summary>
        /// Gets a List of the Files that are available, Stores them in Output.txt, Asyncronous Operation
        /// and can be viewed with FILES()
        /// </summary>
        public async void GetFilesAsync()
        {

            try
            {
                // Get Files 
                var request = DriveServ.Files.List();
                // <file type> text/plain
                //request.Q = "parents in '<parent directory>' and mimeType = '<file type>'";
                request.Q = "mimeType = 'text/plain'";
                Console.WriteLine("Starting New Search");
                var response = await request.ExecuteAsync();
                foreach (var file in response.Files)
                {
                    Console.WriteLine($"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
                    Output.WriteLine("Output.txt", $"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
                }
                Files = (List<GoogleFile>)response.Files;
                Console.WriteLine("Done fetching files");
                Output.WriteLine("Output.txt", "Done fetching files");
                //return response.Files;
            }
            catch (Exception e)
            {
                Output.WriteLine("Output.txt", e.ToString());
            }
        }


        /// <summary>
        /// Gets a List of the Files that are available, Stores them in Output.txt
        /// and can be viewed with FILES()
        /// </summary>
        public List<GoogleFile> GetFiles()
        {
            // Get Files 
            var request = DriveServ.Files.List();
            Console.WriteLine("Recieved Files List form");
            // <file type> text/plain
            //request.Q = "parents in '<parent directory>' and mimeType = '<file type>'";
            request.Q = "mimeType = 'text/plain'";
            Console.WriteLine("Starting New Search");
            var response = request.Execute();
            foreach (var file in response.Files)
            {
                Console.WriteLine($"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
                Output.WriteLine("Output.txt", $"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
            }
            Files = (List<GoogleFile>)response.Files;
            Console.WriteLine("Done fetching files");
            return Files;
            //return response.Files;
        }


        #endregion

        #region File Controller Methods

        static readonly TaskCompletionSource<List<GoogleFile>> taskCompletionSource = new();

        /// <summary>
        /// Confirmation Code: 3.1415901010101010101010101010101010101010101010101
        /// </summary>
        /// <param name="ConfirmationCode"></param>
        /// <returns></returns>
        public async Task<List<GoogleFile>> GetFilesAsync(double ConfirmationCode)
        {
            try
            {
                if (ConfirmationCode == 3.1415901010101010101010101010101010101010101010101)
                {
                    Task<List<GoogleFile>> HandlerFinished = taskCompletionSource.Task;
                    // Get Files 
                    var request = DriveServ.Files.List();
                    // <file type> text/plain
                    //request.Q = "parents in '<parent directory>' and mimeType = '<file type>'";
                    request.Q = "mimeType = 'text/plain'";
                    Console.WriteLine("Starting New Search");
                    var response = await request.ExecuteAsync();
                    foreach (var file in response.Files)
                    {
                        Console.WriteLine($"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
                        Output.WriteLine("Output.txt", $"{file.Id} {file.Name} {file.MimeType} {file.Properties}");
                    }
                    Files = (List<GoogleFile>)response.Files;
                    Console.WriteLine("Done fetching files");
                    Output.WriteLine("Output.txt", "Done fetching files");
                    taskCompletionSource.SetResult(Files);
                    return await HandlerFinished;
                }
                else
                {
                    throw new AccessViolationException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Output.txt", e.ToString());
                Output.WriteLine("Output.txt", e.ToString());
                Console.WriteLine("Connection Error Detected");
                Output.WriteLine("Output.txt", "Connection Error Detected");
            }
            return new List<GoogleFile>();
        }


        /// <summary>
        /// Delete Files Dowloaded
        /// </summary>
        public void DeleteFiles()
        {
            foreach (string file in DownloadedFiles)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch (FileNotFoundException)
                {
                }
            }
        }

        /// <summary>
        /// Delete files downloaded
        /// </summary>
        /// <param name="files"></param>
        public static void DeleteFiles(List<string> files)
        {
            foreach (string file in files)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch (FileNotFoundException) { }
            }
        }


        /// <summary>
        /// Delete the files downloaded
        /// </summary>
        /// <param name="files"></param>
        public static void DeleteFiles(string[] files)
        {
            foreach (string file in files)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch (FileNotFoundException) { }
            }
        }


        /// <summary>
        /// Gets file from  Drive (Shared Included)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>File Stream of Open Path, after downloading the file</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public FileStream GetFilestream(string fileName)
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            string fileId;
            string downloadURL = "";

            var request = driveServ.Files.List();
            // <file type> text/plain
            //request.Q = "parents in '<parent directory>' and mimeType = '<file type>'";
            request.IncludeItemsFromAllDrives = true;
            //request.Q = "mimeType = 'text/plain'";
            Console.WriteLine("Starting New Search");
            var response = request.Execute();
            foreach (GoogleFile file in response.Files)
            {
                if (file.Name == fileName)
                {
                    fileId = file.Id;
                    //if (file.MimeType != null) 
                    //{
                    downloadURL = file.WebContentLink;
                    //}
                    break;
                }
            }
            if (downloadURL != null)
            {
                WebClient webClient = new ();
                webClient.DownloadFile(downloadURL, fileName);
                DownloadedFiles.Add(fileName);
                return (FileStream)webClient.OpenRead(downloadURL);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }


        /// <summary>
        /// Gets file from  Drive (Shared Included)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="OutputFile"></param>
        /// <returns>File Stream of Open Path, after downloading the file</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static void GetFile(string fileName)
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            //string fileId;
            //string downloadURL = "";

            var request = driveServ.Files.List();
            // <file type> text/plain
            //request.Q = "parents in '<parent directory>' and mimeType = '<file type>'";
            //request.IncludeItemsFromAllDrives = true;
            request.Q = "mimeType = 'text/plain'";
            Console.WriteLine("Starting New Search");
            var response = request.Execute();
            //int count = 0;

            if (response.Files.Count(file => file.Name.Equals(fileName)) > 0)
            {
                var downloadFile = response.Files.FirstOrDefault(file => file.Name.Equals(fileName));
                var getRequest = driveServ.Files.Get(downloadFile.Id);

            }

            /*foreach (GoogleFileStore file in response.Files)
            {
                // Console.WriteLine(file.Id);
                // Console.WriteLine(file.Name);
                // Console.WriteLine(file.Name == fileName);
                if (file.Name == fileName)
                {
                    fileId = file.Id;

                    var getRequest = driveServ.Files.Get(fileId);
                    var fileStream = new FileStream(file.Name, FileMode.Create, FileAccess.Write);
                    //await using fileStream = new FileStream(file.Name, FileMode.Create, FileAccess.Write);
                    await getRequest.DownloadAsync(fileStream);
                    fileStream.Close();
                    break;
                }
                count++;
            }*/

        }



        /// <summary>
        /// Gets file from  Drive (Shared Included)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="OutputFile"></param>
        /// <returns>File Stream of Open Path, after downloading the file</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static FileStream GetFileStream(string fileName)
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            string fileId;
            string downloadURL = "";

            var request = driveServ.Files.List();
            // <file type> text/plain
            //request.Q = "parents in '<parent directory>' and mimeType = '<file type>'";
            request.IncludeItemsFromAllDrives = true;
            //request.Q = "mimeType = 'text/plain'";
            Console.WriteLine("Starting New Search");
            var response = request.Execute();
            foreach (GoogleFile file in response.Files)
            {
                if (file.Name == fileName)
                {
                    fileId = file.Id;
                    //if (file.MimeType != null) 
                    //{
                    downloadURL = file.WebContentLink;
                    //}
                    break;
                }
            }
            if (downloadURL != null)
            {
                WebClient webClient = new();
                return (FileStream)webClient.OpenRead(downloadURL);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        /// <summary>
        /// Containts implementation code for Downloading file
        /// var t = new Thread(() => DriveController.GetFileStream("usernames.txt", "usernames.txt"));
        /// t.Start();
        /// </summary>
        public static void Example()
        {
            var t = new Thread(() => GoogleDriveController.DownloadFile("usernames.txt", "usernames.txt"));
            t.Start();
        }

        public static async void DownloadFileAsync(string fileName, string outPutFile)
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            var request = driveServ.Files.List();
            request.Q = "mimeType = 'text/plain''";
            var response = await request.ExecuteAsync();
            if (response.Files.Count(file => file.Name.Equals(fileName)) > 0)
            {
                var downloadFile = response.Files.FirstOrDefault(file => file.Name.Equals(fileName));
                var getRequest = driveServ.Files.Get(downloadFile.Id);
                var fileStream = new FileStream(outPutFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                await getRequest.DownloadAsync(fileStream);
                fileStream.Close();
            }
        }


        public static void DownloadFile(string fileName, string outputFile)
        {
            GoogleCredential credential = GoogleCredential.FromFile("client_secret.json").CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            var request = driveServ.Files.List();
            //request.Q = "mimeType = 'text/plain''";
            var response = request.Execute();
            if (response.Files.Count(file => file.Name.Equals(fileName)) > 0)
            {
                var downloadFile = response.Files.FirstOrDefault(file => file.Name.Equals(fileName));
                var getRequest = driveServ.Files.Get(downloadFile.Id);
                var fileStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                getRequest.Download(fileStream);
                //Stream result = getRequest.ExecuteAsStreamAsync().Result;
                //Stream file = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //result.CopyTo(file);
                fileStream.Close();
            }

        }
        public async void DownloadFileAsync(string fileName)
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            var request = driveServ.Files.List();
            request.Q = "mimeType = 'text/plain''";
            var response = await request.ExecuteAsync();
            if (response.Files.Count(file => file.Name.Equals(fileName)) > 0)
            {
                var downloadFile = response.Files.FirstOrDefault(file => file.Name.Equals(fileName));
                var getRequest = driveServ.Files.Get(downloadFile.Id);
                Stream result = getRequest.ExecuteAsStreamAsync().Result;
                Stream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                await result.CopyToAsync(file);
                file.Close();
            }
        }


        public void DownloadFile(string fileName)
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            var request = driveServ.Files.List();
            request.Q = "mimeType = 'text/plain''";
            var response = request.Execute();
            if (response.Files.Count(file => file.Name.Equals(fileName)) > 0)
            {
                var downloadFile = response.Files.FirstOrDefault(file => file.Name.Equals(fileName));
                var getRequest = driveServ.Files.Get(downloadFile.Id);
                Stream result = getRequest.ExecuteAsStreamAsync().Result;
                Stream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                result.CopyTo(file);
                file.Close();
            }

        }

        #endregion

        #region Basic File Stuff (Read, Edit, List)


        /// <summary>
        /// Prints a list of the files in specified drive
        /// </summary>
        public static void PrintFiles()
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            FileLists files = driveServ.Files.List().Execute();
            List<GoogleFile> fileNames = files.Files.ToList();
            foreach (GoogleFile f in fileNames)
            {
                Console.WriteLine($" {f.Name}, File Id: {f.Id}, {f.MimeType} ");

            }
        }


        /// <summary>
        /// Gets a list of the Google Files (including non-txt items)
        /// </summary>
        /// <returns>List of GoogleFiles</returns>
        public static List<GoogleFile> GetFilesAsList()
        {
            GoogleCredential credential = GoogleCredential.FromFile(ServPath).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService driveServ = new(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            FileLists files = driveServ.Files.List().Execute();
            List<GoogleFile> fileNames = files.Files.ToList();
            return fileNames;
        }

        #endregion
    }


}

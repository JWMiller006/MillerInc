using MillerInc.Convert.Files;
using MillerInc.Convert.Lists;
using MillerInc.UI.OutputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Apis.Drive.v3.Data;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using FileList = Google.Apis.Drive.v3.Data.FileList;
using Google.Apis.Download;

namespace MillerInc.Net.GoogleDrive
{
    public class GoogleDriveFileEditor 
    {

        #region Constructors


        /// <summary>
        /// Creates a new instance of the file editor
        /// </summary>
        /// <param name="googleFile">The reference to the GoogleFileStore</param>
        public GoogleDriveFileEditor(GoogleFile googleFile)
        {
            GoogleFileStore = googleFile;
            FileName = GoogleFileStore.Name;
            FileId = GoogleFileStore.Id;
            NewService();
            GoogleDriveControl.DownloadFile(FileName, FileName, KeyFile);
            Lines = FileToList.FileToListConverter(FileName);
        }


        /// <summary>
        /// Creates a new instance of the file editor
        /// </summary>
        /// <param name="googleFile">Reference to the GoogleFileStore that you want to edit</param>
        /// <param name="keyFile">Path to the KeyFile</param>
        public GoogleDriveFileEditor(GoogleFile googleFile, string keyFile)
        {
            GoogleFileStore = googleFile;
            FileName = GoogleFileStore.Name;
            FileId = GoogleFileStore.Id;
            KeyFile = keyFile;
            NewService();
            GoogleDriveControl.DownloadFile(FileName, FileName, KeyFile);
            Lines = FileToList.FileToListConverter(FileName);
        }


        /// <summary>
        /// Creates a new FileEditor Instance with the fileName or Id and specifed key file
        /// </summary>
        /// <param name="fileName">Name of the file that you want to edit</param>
        /// <param name="keyFile">Path to the Key File</param>
        public GoogleDriveFileEditor(string fileName, string keyFile)
        {
            KeyFile = keyFile;
            NewService();
            GoogleFileStore = GetFile(fileName);
            FileName = fileName;
            FileId = GoogleFileStore.Id;
            GoogleDriveControl.DownloadFile(FileName, FileName, KeyFile);
            Lines = FileToList.FileToListConverter(FileName);
        }


        /// <summary>
        /// Creates a new Instance of the File Editor
        /// </summary>
        /// <param name="fileName">Name of the file that you want to edit</param>
        public GoogleDriveFileEditor(string fileName)
        {
            NewService();
            GoogleFileStore = GetFile(fileName);
            FileName = fileName;
            FileId = GoogleFileStore.Id;
            GoogleDriveControl.DownloadFile(FileName, FileName, KeyFile);
            Lines = FileToList.FileToListConverter(FileName);
        }


        #endregion


        #region Fields



        /// <summary>
        /// The Stored GoogleFile
        /// </summary>
        public GoogleFile GoogleFileStore { get; set; }



        /// <summary>
        /// The Name of the file
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// The Id Of the File
        /// </summary>
        public string FileId { get; set; }


        /// <summary>
        /// The Path to the KeyFile
        /// </summary>
        private string KeyFile { get; set; } = "client_secret.json";


        /// <summary>
        /// The DriveService for the specific drive
        /// </summary>
        private DriveService DriveServ { get; set; }


        /// <summary>
        /// The List of all the lines that are in the file
        /// </summary>
        public List<string> Lines { get; set; } = new();


        #endregion


        #region Internal Methods in order to get and set the file



        /// <summary>
        /// Gets the GoogleFileStore with the predetermined GoogleService Created
        /// </summary>
        /// <param name="fileName">The name or Id of the file</param>
        /// <returns>The data and reference to the Google File</returns>
        private GoogleFile GetFile(string fileName)
        {
            GoogleFile storedFile = new();
            var request = DriveServ.Files.List();
            Output.WriteLine("output.txt", "Starting New Search");
            var response = request.Execute();
            foreach (var file in response.Files)
            {
                if ((file.Name == fileName) || (file.Id == fileName))
                {
                    storedFile = file;
                }
            }
            return storedFile;
        }



        /// <summary>
        /// Gets a new driveservice from the specified file key
        /// </summary>
        /// <param name="keyFile">The path to the key file</param>
        /// <returns>A new DriveServ</returns>
        private void NewService()
        {
            var credentials = GoogleCredential.FromFile(KeyFile).CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });
            DriveServ = driveService;
        }

        #endregion


        #region File Editing Methods 



        /// <summary>
        /// Appends a line to the end of the file
        /// </summary>
        /// <param name="line">The line to append to the file</param>
        public void WriteLine(string line)
        {
            Lines.Add(line);
        }


        /// <summary>
        /// Appends text to the end of the last line
        /// </summary>
        /// <param name="text">The text to append to the file</param>
        public void Write(string text)
        {
            Lines[Lines.Count - 1] += text;
        }



        /// <summary>
        /// Appends text to end of the line specified
        /// </summary>
        /// <param name="text">The Text to overwrite the line</param>
        /// <param name="line">The index of the line</param>
        public void WriteLine(string text, int line)
        {
            Lines[line] += text;
        }



        /// <summary>
        /// Over-Writes the entire file
        /// </summary>
        /// <param name="lines">The entire data of the file that will overwrite the file with</param>
        public void OverWrite(List<string> lines)
        {
            Lines = lines;
        }



        /// <summary>
        /// Over-Writes the last line in the file
        /// </summary>
        /// <param name="line">The Text to overwrite with </param>
        public void OverWriteLine(string line)
        {
            Lines[Lines.Count - 1] = line;
        }



        /// <summary>
        /// Overwrites the specified line
        /// </summary>
        /// <param name="line">The text of to be overwriting the line</param>
        /// <param name="lineNum">The index of the line to be overwritten</param>
        public void OverWriteLine(string line, int lineNum)
        {
            Lines[lineNum] = line;
        }


        /// <summary>
        /// Inserts the line at the index given 
        /// </summary>
        /// <param name="line">The line that is going to inserted</param>
        /// <param name="index">he Index of the line where it is suppoesed to be inserted</param>
        public void InsertLine(string line, int index)
        {
            Lines.Insert(index, line);
        }

        /// <summary>
        /// Deletes the line at the given index
        /// </summary>
        /// <param name="index">Index of line to delete</param>
        public void DeleteLine(int index)
        {
            Lines.RemoveAt(index);
        }


        /// <summary>
        /// Updates the file and uploads the changes
        /// </summary>
        public void UpdateFile()
        {
            System.IO.File.WriteAllLines(FileName, Lines.ToArray());
            GoogleFile file = new()
            {
                Name = FileName,
                Id = FileId,
                Description = GoogleFileStore.Description,
                DriveId = GoogleFileStore.DriveId,
                MimeType = GoogleFileStore.MimeType,
                LastModifyingUser = new Google.Apis.Drive.v3.Data.User(),
                Owners = GoogleFileStore.Owners,
                OwnedByMe = GoogleFileStore.OwnedByMe,
                Parents = GoogleFileStore.Parents,
                PermissionIds = GoogleFileStore.PermissionIds,
                SharingUser = GoogleFileStore.SharingUser,
                Permissions = GoogleFileStore.Permissions,
                AppProperties = GoogleFileStore.AppProperties,
                CopyRequiresWriterPermission = GoogleFileStore.CopyRequiresWriterPermission
            };
            Stream fStream = System.IO.File.OpenRead(file.Name);
            var request = DriveServ.Files.Create(file, fStream, file.MimeType);
            var response = request.Upload();
            if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
            {
                throw response.Exception;
            }
            System.IO.File.Delete(FileName);
            GoogleDriveControl.DownloadFile(FileName, FileName, KeyFile);
        }



        #endregion


        #region Overrides 


        public override string ToString()
        {
            return "File Name: " + FileName + "\nFile ID: " + FileId + "\nLines:\n\n"
                + ListToString.ListString(Lines, "\n", "\n\n");
        }


        public override bool Equals(object obj)
        {
            try
            {
                if (obj.ToString() == ToString())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        #endregion

    }
}

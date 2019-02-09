using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace RequestHelperSample.Data.Helpers
{
    public static class FileHelper
    {
        private static string _folderPath { get; set; }
        private static string _rootPath { get; set; }

        public static void Initialize(string path, string rootPath)
        {
            _folderPath = path;
            _rootPath = rootPath;
        }

        public static string FileSavePath
        {
            get { return $"{_rootPath}\\{_folderPath}"; }
        }

        public static string SaveFile(IFormFile file)
        {

            var fileNameGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(FileSavePath, fileNameGuid);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fileNameGuid;
        }

        public static DocumentModel GetFileModel(string fileName)
        {
            var path = Path.Combine(FileSavePath, fileName);
            return new DocumentModel()
            {
                Name = fileName,
                Stream = new FileStream(path, FileMode.Open)
            };
        }

        public static Stream GetFileStream(string fileName)
        {
            var path = Path.Combine(FileSavePath, fileName);
            return new FileStream(path, FileMode.Open);
        }

        public static void DeleteFile(string fileName)
        {
            try
            {
                var path = Path.Combine(FileSavePath, fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch { }
        }

        public static void DeleteFiles(params string[] files)
        {
            foreach (var file in files)
            {
                DeleteFile(file);
            }
        }
    }

    public class DocumentModel
    {
        public string Name { get; set; }

        public Stream Stream { get; set; }
    }
}

using DigitalGoods.Core;
using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using System.IO;

namespace DigitalGoods.Infrastructure.Data
{
    public class FileManager : IFileManager
    {
        private static string s_folderName = "MediaStore";

        private readonly string _externalPath;

        private readonly List<string> _addedFiles;

        private readonly List<string> _createdFolders;

        public FileManager()
        {
            _externalPath = System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot", s_folderName);
            _addedFiles = new List<string>();
            _createdFolders = new List<string>();
        }

        public bool FileExist(string path)
        {
            var fullPath = System.IO.Path.Combine(_externalPath, path);
            return File.Exists(fullPath);
        }

        public async Task Save(string internalPath, Func<FileStream, Task> saveAction)
        {
            await CreateDirectories(internalPath);
            string fullPath = System.IO.Path.Combine(_externalPath, internalPath);

            await using FileStream fs = new(fullPath, FileMode.Create);
            await saveAction(fs);

            _addedFiles.Add(fullPath);
        }

        private async Task CreateDirectories(string internalPath)
        {
            var allFolders = internalPath.Split(@"\").ToList();
            var allPathes = AllPathes(allFolders);
            foreach (var path in allPathes)
            {
                var fullPath = System.IO.Path.Combine(_externalPath, path);
                if (Directory.Exists(fullPath) is false)
                {
                    await Task.Run(() => Directory.CreateDirectory(fullPath));
                    _createdFolders.Add(fullPath);
                }
            }
        }

        private List<string> AllPathes(List<string> folderNames)
        {
            var result = new List<string>();
            for (int i = 1; i < folderNames.Count; i++)
            {
                var path = System.IO.Path.Combine(folderNames.Take(i).ToArray());
                result.Add(path);
            }
            return result;
        }

        public async Task RollBack()
        {
            foreach (var path in _addedFiles)
            {
                await Task.Run(() => File.Delete(path));
            }
            foreach(var path in _createdFolders)
            {
                await Task.Run(() => Directory.Delete(path));
            }
        }

        public string FilePath(Media media)
        {
            return System.IO.Path.Combine(s_folderName, media.Path);
        }

        public async Task Delete(string path)
        {
            path = Path.Combine(_externalPath, path);
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path));
            }
        }
    }
}

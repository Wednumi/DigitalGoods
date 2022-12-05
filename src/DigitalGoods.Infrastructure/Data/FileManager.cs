using DigitalGoods.Core;
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
            _externalPath = Path.Combine(Environment.CurrentDirectory, s_folderName);
            _addedFiles = new List<string>();
            _createdFolders = new List<string>();
        }

        public bool FileExist(string path)
        {
            var fullPath = Path.Combine(_externalPath, path);
            return File.Exists(fullPath);
        }

        public async Task Save(string internalPath, Func<FileStream, Task> saveAction)
        {
            await CreateDirectories(internalPath);
            string fullPath = Path.Combine(_externalPath, internalPath);

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
                var fullPath = Path.Combine(_externalPath, path);
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
                var path = Path.Combine(folderNames.Take(i).ToArray());
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
    }
}

using DigitalGoods.Infrastructure.Data;
using System.Text;

namespace DigitalGoods.IntegrationTests.Tests
{
    public class FileHandling
    {
        [Fact]
        public void FileManager_stores_files()
        {
            ClearMainFolder();
            var path = @"first-folder\second-folder\some-file.txt";
            var fileText = "test text";
            var savingAction = async (FileStream fs) =>
            {
                await Task.Run(() => WriteText(fs, fileText));
            };
            var sut = new FileManager();

            sut.Save(path, savingAction).Wait();

            var fullPath = Path.Combine(ExtenalPath(), path);
            var textRead = File.ReadAllText(fullPath);
            Assert.Equal(fileText, textRead);
        }

        [Fact]
        public void FileManager_rollback_successful()
        {
            ClearMainFolder();
            var path = @"first-folder\some-file.txt";
            var fileText = "test text";
            var savingAction = async (FileStream fs) =>
            {
                await Task.Run(() => WriteText(fs, fileText));
            };
            var sut = new FileManager();

            sut.Save(path, savingAction).Wait();
            sut.RollBack().Wait();

            var directoryClear = Directory.GetDirectories(ExtenalPath()).Count() == 0;
            Assert.True(directoryClear);
        }

        [Fact]
        public void FileManager_rollback_dont_delete_extra_needed()
        {
            ClearMainFolder();
            var pathToRemain = @"first-folder\second-folder\file-not-to-delete.txt";
            var fileText = "test text";
            var savingAction = async (FileStream fs) =>
            {
                await Task.Run(() => WriteText(fs, fileText));
            };
            var previousFileManager = new FileManager();
            previousFileManager.Save(pathToRemain, savingAction).Wait();
            var pathToDelete = @"first-folder\second-folder\third-folder\file-to-delete.txt";
            var sut = new FileManager();

            sut.Save(pathToDelete, savingAction).Wait();
            sut.RollBack().Wait();

            var fileToRemainExistance = File.Exists(Path.Combine(ExtenalPath(), pathToRemain));
            Assert.True(fileToRemainExistance);
        }

        private void WriteText(FileStream fs, string fileText)
        {
            var text = new UTF8Encoding(true).GetBytes(fileText);
            fs.Write(text);
        }

        private string ExtenalPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\MediaStore");
        }

        private void ClearMainFolder()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var folderPath = currentPath + @"\wwwroot\MediaStore";
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
    }
}

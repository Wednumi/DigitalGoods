namespace DigitalGoods.UnitTests.Mocks
{
    public class FileManagerMock : IFileManager
    {
        public List<string> Files { get; set; }

        public FileManagerMock()
        {
            Files = new List<string>();
        }

        public async Task DeleteAsync(string path)
        {
            await Task.Run(() => Files.Remove(path));
        }

        public bool FileExist(string path)
        {
            throw new NotImplementedException();
        }

        public string GetFullPath(Media media)
        {
            throw new NotImplementedException();
        }

        public Task RollBackAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(string path, Func<FileStream, Task> saveAction)
        {
            await Task.Run(() => Files.Add(path));
        }
    }
}

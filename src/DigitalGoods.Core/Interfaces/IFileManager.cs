using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IFileManager
    {
        public bool FileExist(string path);

        public Task SaveAsync(string path, Func<FileStream, Task> saveAction);

        public Task DeleteAsync(string path);

        public Task RollBackAsync();

        public string GetFullPath(Media media);
    }
}

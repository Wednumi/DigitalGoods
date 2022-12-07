using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IFileManager
    {
        public bool FileExist(string path);

        public Task Save(string path, Func<FileStream, Task> saveAction);

        public Task Delete(string path);

        public Task RollBack();

        public string FilePath(Media media);
    }
}

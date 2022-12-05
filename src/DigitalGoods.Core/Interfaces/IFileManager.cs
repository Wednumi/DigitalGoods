namespace DigitalGoods.Core.Interfaces
{
    public interface IFileManager
    {
        public bool FileExist(string path);

        public Task Save(string path, Func<FileStream, Task> saveAction);

        public Task RollBack();
    }
}

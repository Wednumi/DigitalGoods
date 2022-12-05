using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;

namespace DigitalGoods.Core.Services
{
    public class PathGenerator
    {
        private readonly IFileManager _fileManager;

        public PathGenerator(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string Generate(string contentType)
        {
            string result;

            var dateDirectory = DateTime.Today.ToString("yyyy-MM");
            var extension = "." + GetFileExtension(contentType);

            do
            {
                var randName = Path.GetRandomFileName();
                result = Path.Combine(dateDirectory, randName) + extension;
            }
            while (_fileManager.FileExist(result));

            return result;
        }

        private string GetFileExtension(string type)
        {
            var splited = type.Split('/');
            return splited.Last();
        }
    }
}

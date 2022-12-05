using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;

namespace DigitalGoods.Core.Services
{
    public class MediaService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Media> _mediaRepository;

        private readonly IFileManager _fileManager;

        private readonly PathGenerator _pathGenerator;

        public MediaService(IRepositoryFactory repositoryFactory, IFileManager fileManager)
        {
            _repositoryFactory = repositoryFactory;
            _mediaRepository = _repositoryFactory.CreateRepository<Media>();
            _fileManager = fileManager;
            _pathGenerator = new PathGenerator(fileManager);
        }

        public async Task<ActionResult> Store(Media media, Func<FileStream, Task> saveAction)
        {
            try
            {
                media.Path = _pathGenerator.Generate(media.ContentType);

                await _fileManager.Save(media.Path, saveAction);
                await _mediaRepository.UpdateAsync(media);

                return new ActionResult(true);
            }
            catch(Exception ex)
            {
                await _fileManager.RollBack();
                return new ActionResult(false, ex.Message);
            }
        }
    }
}

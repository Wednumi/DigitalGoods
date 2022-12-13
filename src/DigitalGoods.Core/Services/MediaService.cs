using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class MediaService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Media> _mediaRepository;

        private readonly IFileManager _fileManager;

        private readonly PathGenerator _pathGenerator;

        private ICollection<Media> _medias = null!;

        private ICollection<Media> _mediasBefore = null!;

        public MediaService(IRepositoryFactory repositoryFactory, IFileManager fileManager)
        {
            _repositoryFactory = repositoryFactory;
            _mediaRepository = _repositoryFactory.CreateRepository<Media>();
            _fileManager = fileManager;
            _pathGenerator = new PathGenerator(fileManager);
        }

        public void SetMedias(ICollection<Media> medias)
        {
            _medias = medias;
            _mediasBefore = new List<Media>(_medias);
        }

        public async Task<ActionResult> Save(Media media, Func<FileStream, Task> saveAction)
        {
            media.Path = _pathGenerator.Generate(media.ContentType);

            try
            {
                await _fileManager.Save(media.Path, saveAction);
                await _mediaRepository.UpdateAsync(media);
                return new ActionResult(true);
            }
            catch(Exception ex)
            {
                await _fileManager.Delete(media.Path);
                return new ActionResult(false, ex.Message);
            }
        }

        public async Task Delete(Media media)
        {
            await _mediaRepository.DeleteAsync(media);
            await _fileManager.Delete(media.Path);
        }

        public async Task RollBack()
        {
            var added = _medias.Except(_mediasBefore).ToList();
            await _mediaRepository.DeleteRangeAsync(added);
            await _fileManager.RollBack();
        }
    }
}

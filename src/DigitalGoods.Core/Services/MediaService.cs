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

        private List<Media> _medias;

        private List<Media> _mediasBefore;

        public MediaService(IRepositoryFactory repositoryFactory, IFileManager fileManager)
        {
            _repositoryFactory = repositoryFactory;
            _mediaRepository = _repositoryFactory.CreateRepository<Media>();
            _fileManager = fileManager;
            _pathGenerator = new PathGenerator(fileManager);
            _medias = new List<Media>();
            _mediasBefore = new List<Media>();
        }

        public async Task<List<Media>> InitializedMedias(Offer offer)
        {
            _medias = await _mediaRepository.ListAsync(new MediaByOfferSpec(offer)) ?? _medias;
            _mediasBefore = new List<Media>(_medias);
            return _medias;
        }

        public async Task<ActionResult> Save(Media media, Func<FileStream, Task> saveAction)
        {
            media.Path = _pathGenerator.Generate(media.ContentType);

            var savedToDb = false;
            try
            {
                await _fileManager.Save(media.Path, saveAction);
                savedToDb = await SaveToDbResult(media);
                _medias.Add(media);
                return new ActionResult(true);
            }
            catch(Exception ex)
            {
                await Delete(media, savedToDb);
                return new ActionResult(false, ex.Message);
            }
        }

        private async Task<bool> SaveToDbResult(Media media)
        {
            await _mediaRepository.UpdateAsync(media);
            return true;
        }

        public async Task Delete(Media media, bool deleteFromDb = true)
        {
            if (deleteFromDb)
            {
                await _mediaRepository.DeleteAsync(media);
            }
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

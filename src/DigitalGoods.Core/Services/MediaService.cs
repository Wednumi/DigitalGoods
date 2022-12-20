using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class MediaService : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Media> _mediaRepository;

        private readonly IFileManager _fileManager;

        private readonly IPathGenerator _pathGenerator;

        private ICollection<Media> _mediasBefore = null!;

        public ICollection<Media> Medias { get; set; } = null!;

        public MediaService(IRepositoryFactory repositoryFactory, IFileManager fileManager, 
            IPathGenerator pathGenerator, IRollBackContainer rollBackContainer)
            : base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _mediaRepository = _repositoryFactory.CreateRepository<Media>();
            _fileManager = fileManager;
            _pathGenerator = pathGenerator;
        }

        public void Initialize(ICollection<Media> medias)
        {
            Medias = medias;
            _mediasBefore = new List<Media>(Medias);
        }

        public async Task<ActionResult> SaveAsync(Media media, Func<FileStream, Task> saveAction)
        {
            media.Path = _pathGenerator.Generate(media.ContentType);

            try
            {
                await _fileManager.SaveAsync(media.Path, saveAction);
                await _mediaRepository.UpdateAsync(media);
                return new ActionResult(true);
            }
            catch(Exception ex)
            {
                await _fileManager.DeleteAsync(media.Path);
                return new ActionResult(false, ex.Message);
            }
        }

        public async Task DeleteAsync(Media media)
        {
            await _mediaRepository.DeleteAsync(media);
            await _fileManager.DeleteAsync(media.Path);
        }

        protected override async Task RollBackAsync()
        {
            var added = Medias.Except(_mediasBefore).ToList();
            await _mediaRepository.DeleteRangeAsync(added);
            await _fileManager.RollBackAsync();
        }

        public void SetPreview(Media media)
        {
            ClearPreview();
            media.IsPreview = true;
        }

        private void ClearPreview()
        {
            foreach(var media in Medias)
            {
                media.IsPreview = false;
            }
        }
    }
}

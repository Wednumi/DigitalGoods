using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services.TagServices
{
    public class TagCreator : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Tag> _repository;

        private readonly ICollection<Tag> _createdTags;

        public TagCreator(IRepositoryFactory repositoryFactory, IRollBackContainer rollBackContainer)
            : base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Tag>();
            _createdTags = new HashSet<Tag>();
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            var sameTag = await TryFindSameAsync(tag);
            if (sameTag is null)
            {
                await _repository.UpdateAsync(tag);
                _createdTags.Add(tag);
            }
            else
            {
                tag = sameTag;
            }
            return tag;
        }

        private async Task<Tag?> TryFindSameAsync(Tag tag)
        {
            return await _repository.FirstOrDefaultAsync(new TagByNameSpec(tag.Name));
        }

        protected override async Task RollBackAsync()
        {
            foreach (var tag in _createdTags)
            {
                if (await CanBeDeletedAsync(tag))
                {
                    await _repository.DeleteAsync(tag);
                }
            }
        }

        private async Task<bool> CanBeDeletedAsync(Tag tag)
        {
            var offerRepository = _repositoryFactory.CreateRepository<Offer>();
            var offersUsing = await offerRepository.CountAsync(new OffersUsingTagSpec(tag));
            return offersUsing == 0;
        }
    }
}

using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class TagService : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Tag> _repository;

        private readonly ICollection<Tag> _createdTags;

        public Category? Category { get; private set; }

        public ICollection<Tag> Tags { get; set; } = null!;

        public ICollection<Tag> PossibleTags { get; set; } = null!;

        public TagService(IRepositoryFactory repositoryFactory, IRollBackContainer rollBackContainer)
            :base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Tag>();
            _createdTags = new HashSet<Tag>();
        }

        public async Task InitializeAsync(ICollection<Tag> tags, Category? category)
        {
            Tags = tags;
            IfNoTagsAttachedSet(category);
            await SetPossibleTags(category);
        }

        private void IfNoTagsAttachedSet(Category? category)
        {
            Category = Tags.Count == 0
                ? category
                : GetTagsCategory();
        }

        private Category? GetTagsCategory()
        {
            var categories = Tags.Select(t => t.Category).Distinct().ToList();
            var result = categories.Count == 1
                ? categories.First()
                : throw new Exception("Failure when identifying tags' category");
            return result;
        }

        private async Task SetPossibleTags(Category? category)
        {
            PossibleTags = await _repository.ListAsync(new TagsForCategorySpec(category));
        }

        public async Task Reset(Category? newCategory)
        {
            Tags.Clear();
            Category = newCategory;
            await SetPossibleTags(newCategory);
        }

        public async Task AddAsync(Tag tag)
        {
            Tags.Add(tag);
            await _repository.SaveChangesAsync();
        }

        public void Remove(Tag tag)
        {
            Tags.Remove(tag);
        }

        public async Task CreateAsync(Tag tag)
        {
            var sameTag = await TryFindSameAsync(tag);
            if(sameTag is null)
            {
                await _repository.UpdateAsync(tag);
                _createdTags.Add(tag);
            }
            else
            {
                tag = sameTag;
            }
            await AddAsync(tag);
        }

        private async Task<Tag?> TryFindSameAsync(Tag tag)
        {
            return await _repository.FirstOrDefaultAsync(new TagByNameSpec(tag.Name));
        }

        protected override async Task RollBackAsync()
        {
            if(_createdTags.Count == 0)
            {
                return;
            }
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

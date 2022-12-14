using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class TagService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Tag> _repository;

        private Category? _category;

        public ICollection<Tag> Tags = null!;

        public ICollection<Tag> PossibleTags { get; set; } = null!;

        public TagService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Tag>();
        }

        public async Task Initialize(ICollection<Tag> tags, Category? category)
        {
            _category = category;
            Tags = tags;            
            PossibleTags = await _repository.ListAsync(new TagsForCategorySpec(category));
        }

        public void Add(Tag tag)
        {
            Tags.Add(tag);
        }

        public void Remove(Tag tag)
        {
            Tags.Remove(tag);
        }
    }
}

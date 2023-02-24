using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services.TagServices
{
    public class PossibleTagsViewer
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Tag> _repository;

        public PossibleTagsViewer(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Tag>();
        }

        public async Task<List<Tag>> PossibleTags(Category? category)
        {
            return await _repository.ListAsync(new TagsForCategorySpec(category));
        }
    }
}

using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services.CategoryServices
{
    public class CategoryCreator : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Category> _repository;

        private readonly List<Category> _created;

        public CategoryCreator(IRepositoryFactory repositoryFactory, IRollBackContainer rollBackContainer)
            : base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Category>();
            _created = new List<Category>();
        }

        public async Task<Category> CreateCategoryAsync(Category toCreate)
        {
            var sameCategory = await TryFindSameAsync(toCreate);
            if (sameCategory is null)
            {
                await _repository.UpdateAsync(toCreate);
                _created.Add(toCreate);
            }
            else
            {
                toCreate = sameCategory;
            }
            return toCreate;
        }

        private async Task<Category?> TryFindSameAsync(Category category)
        {
            return await _repository.FirstOrDefaultAsync(new CategoryByNameSpec(category.Name));
        }

        protected override async Task RollBackAsync()
        {
            foreach (var category in _created)
            {
                if (await CanBeDeletedAsync(category))
                {
                    await _repository.DeleteAsync(category);
                }
            }
        }

        private async Task<bool> CanBeDeletedAsync(Category category)
        {
            var offerRepository = _repositoryFactory.CreateRepository<Offer>();
            var offersUsing = await offerRepository.CountAsync(new OffersUsingCategorySpec(category));
            return offersUsing == 0;
        }
    }
}

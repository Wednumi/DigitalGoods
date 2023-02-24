using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services.CategoryServices
{
    public class CategoryTreeViewer
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Category> _repository;

        public Stack<Category> CategoryTree { get; set; }

        public CategoryTreeViewer(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Category>();
            CategoryTree = new Stack<Category>();
        }

        public async Task InitializeAsync(Category? current)
        {
            if (current is null)
            {
                return;
            }
            await AddParentsForAsync(current);
            CategoryTree.Push(current);
        }

        private async Task AddParentsForAsync(Category category)
        {
            var parentList = await GetParentsForAsync(category);
            CategoryTree = new Stack<Category>(parentList);
        }

        private async Task<List<Category>> GetParentsForAsync(Category category)
        {
            var parent = await GetParentAsync(category);
            var list = new List<Category>();

            while (parent is not null)
            {
                list.Add(parent);
                parent = await GetParentAsync(parent);
            }
            list.Reverse();

            return list;
        }

        private async Task<Category?> GetParentAsync(Category category)
        {
            var specification = new CategoryParentSpec(category);
            return await _repository.FirstOrDefaultAsync(specification);
        }

        public void Push(Category category)
        {
            CategoryTree.Push(category);
        }

        public Category? PopPrevious()
        {
            CategoryTree.TryPop(out var current);
            CategoryTree.TryPop(out var previous);
            return previous;
        }
    }
}

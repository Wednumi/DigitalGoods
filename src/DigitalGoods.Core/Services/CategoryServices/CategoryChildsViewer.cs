using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services.CategoryServices
{
    public class CategoryChildsViewer
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Category> _repository;

        public CategoryChildsViewer(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Category>();
        }

        public async Task<List<Category>> ChildsForAsync(Category? category)
        {
            return await _repository.ListAsync(new CategoryChildsSpec(category));
        }

        public async Task<ICollection<Category>> AllChildsAsync(Category category, 
            ICollection<Category>? result = null)
        {
            result ??= new List<Category>() { category! };
            var childs = await ChildsForAsync(category);
            foreach (var child in childs)
            {
                result.Add(child);
                await AllChildsAsync(child, result);
            }
            return result;
        }
    }
}

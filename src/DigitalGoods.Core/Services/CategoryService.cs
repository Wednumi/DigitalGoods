using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class CategoryService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Category> _repository;

        public Stack<Category> Parents { get; private set; } = new Stack<Category>();

        public ICollection<Category> Childs { get; private set; } = null!;

        public CategoryService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Category>();
        }

        public async Task MoveCurrentTo(Category? category)
        {
            await SetChilds(category);
            await SetParents(category);
        }

        private async Task SetChilds(Category? parent)
        {
            int? parentId = parent?.Id;
            Childs = await _repository.ListAsync(new ChildsForCategorySpec(parentId));
        }

        private async Task SetParents(Category? category)
        {
            if(category is null)
            {
                Parents = new Stack<Category>();
                return;
            }

            Parents.TryPeek(out Category? lastParent);

            var findedParents = new List<Category>();
            
            while (category!.ParentId != lastParent?.Id)
            {                
                CategoryParentSpec specification = new(category);
                category = await _repository.FirstOrDefaultAsync(specification);
                if(category is null)
                {
                    break;
                }
                findedParents.Add(category);
            }

            findedParents.Reverse();
            findedParents.ForEach(p => Parents.Push(p));
        }

        public async Task<Category?> ReturnToLast()
        {
            Parents.TryPop(out Category? last);
            await SetChilds(last);
            return last;
        }
    }
}

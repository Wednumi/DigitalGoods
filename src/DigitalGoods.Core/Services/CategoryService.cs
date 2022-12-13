using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class CategoryService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Category> _repository;

        public Stack<Category> Parents { get; private set; } 

        public ICollection<Category> Childs { get; private set; } 

        public Category? Current { get; set; }

        public Func<Category?, Task> CurrentChanged { get; set; } = null!;

        public CategoryService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Category>();

            Parents = new Stack<Category>();
            Childs = new List<Category>();
        }

        public async Task Initialize(Category? category, Func<Category?, Task> currentChaned)
        {
            CurrentChanged += currentChaned;
            await ChangeCurrent(category);
            await SetChilds();
            await SetParents();
        }

        private async Task ChangeCurrent(Category? current)
        {
            Current = current;
            await CurrentChanged.Invoke(Current);
            await SetChilds();
        } 

        private async Task SetChilds()
        {
            int? parentId = Current?.Id;
            Childs = await _repository.ListAsync(new ChildsForCategorySpec(parentId));
        }

        private async Task SetParents()
        {
            if(Current is null)
            {
                return;
            }

            var parent = await GetParent(Current);

            while (parent is not null)
            {
                Parents.Push(parent);
                parent = await GetParent(parent);
            }
            Parents.Reverse();
        }

        private async Task<Category?> GetParent(Category category)
        {
            var specification = new CategoryParentSpec(category);
            var parent = await _repository.FirstOrDefaultAsync(specification);
            return parent;
        }

        public async Task MoveTo(Category category)
        {
            if(Current is not null)
            {
                Parents.Push(Current);
            }
            await ChangeCurrent(category);
        }

        public async Task ReturnToLast()
        {
            Parents.TryPop(out Category? last);
            await ChangeCurrent(last);
        }

        public async Task MoveToAdded(Category toAdd)
        {
            await _repository.UpdateAsync(toAdd);
            await AlterTreeAfterAdding(toAdd);
        }

        private async Task AlterTreeAfterAdding(Category added)
        {
            if(Current is not null)
            {
                Parents.Push(Current);
            }
            await ChangeCurrent(added);
        }
    }
}

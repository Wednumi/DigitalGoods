using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;
using System.Collections.Generic;

namespace DigitalGoods.Core.Services
{
    public class CategoryService : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Category> _repository;

        private List<Category> _added;

        public Stack<Category> Parents { get; private set; } 

        public ICollection<Category> Childs { get; private set; } 

        public Category? Current { get; set; }

        public Action<Category?> CurrentChanged { get; set; } = null!;

        public CategoryService(IRepositoryFactory repositoryFactory, IRollBackContainer rollBackContainer)
            :base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Category>();

            _added = new List<Category>();
            Parents = new Stack<Category>();
            Childs = new List<Category>();
        }

        public async Task InitializeAsync(Category? category, Action<Category?> currentChaned)
        {
            CurrentChanged += currentChaned;
            ChangeCurrent(category);
            await SetChildsAsync();
            await SetParentsAsync();
        }

        private async Task SetChildsAsync()
        {
            int? parentId = Current?.Id;
            Childs = await _repository.ListAsync(new ChildsForCategorySpec(parentId));
        }

        private async Task SetParentsAsync()
        {
            if(Current is null)
            {
                return;
            }

            var parentList = await GetParentTreeAsync();
            Parents = new Stack<Category>(parentList);
        }

        private async Task<List<Category>> GetParentTreeAsync()
        {
            var parent = await GetParentAsync(Current!);
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
            var parent = await _repository.FirstOrDefaultAsync(specification);
            return parent;
        }

        public async Task MoveToAsync(Category category)
        {
            if(Current is not null)
            {
                Parents.Push(Current);
            }
            await ChangeCurrentWithChildsAsync(category);
        }

        public async Task ReturnToLastAsync()
        {
            Parents.TryPop(out Category? last);
            await ChangeCurrentWithChildsAsync(last);
        }

        public async Task MoveToAddedAsync(Category toAdd)
        {
            await _repository.UpdateAsync(toAdd);
            ChangeCurrentAfterAdding(toAdd);
            _added.Add(toAdd);
        }

        private async Task ChangeCurrentWithChildsAsync(Category? newCurrent)
        {
            ChangeCurrent(newCurrent);
            await SetChildsAsync();
        }

        private void ChangeCurrentAfterAdding(Category added)
        {
            if (Current is not null)
            {
                Parents.Push(Current);
            }
            ChangeCurrent(added);
            Childs = new List<Category>();
        }

        private void ChangeCurrent(Category? newCurrent)
        {
            Current = newCurrent;
            CurrentChanged.Invoke(Current);
        }

        protected override async Task RollBackAsync()
        {
            await _repository.DeleteRangeAsync(_added);
        }
    }
}

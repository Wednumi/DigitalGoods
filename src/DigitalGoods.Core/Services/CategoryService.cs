using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class CategoryService : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Category> _repository;

        private List<Category> _created;

        public Stack<Category> Parents { get; private set; } 

        public ICollection<Category> Childs { get; private set; } 

        public Category? Current { get; set; }

        public Action<Category?> CurrentChanged { get; set; } = null!;

        public CategoryService(IRepositoryFactory repositoryFactory, IRollBackContainer rollBackContainer)
            :base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<Category>();

            _created = new List<Category>();
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

        private void ChangeCurrent(Category? newCurrent)
        {
            Current = newCurrent;
            CurrentChanged.Invoke(Current);
        }

        private async Task SetChildsAsync()
        {
            int? parentId = Current?.Id;
            Childs = await _repository.ListAsync(new CategoryChildsSpec(parentId));
        }

        private async Task SetParentsAsync()
        {
            if(Current is null)
            {
                return;
            }

            var parentList = await GetParentTreeAsync(Current);
            Parents = new Stack<Category>(parentList);
        }

        private async Task<List<Category>> GetParentTreeAsync(Category current)
        {
            var parent = await GetParentAsync(current);
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

        private async Task ChangeCurrentWithChildsAsync(Category? newCurrent)
        {
            ChangeCurrent(newCurrent);
            await SetChildsAsync();
        }

        public async Task ReturnToLastAsync()
        {
            Parents.TryPop(out Category? last);
            await ChangeCurrentWithChildsAsync(last);
        }

        public async Task MoveToCreatedAsync(Category toCreate)
        {
            var created = await CreateCategoryAsync(toCreate);
            ChangeTreeAfterCreating(created);
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

        private void ChangeTreeAfterCreating(Category added)
        {
            if (Current is not null)
            {
                Parents.Push(Current);
            }
            ChangeCurrent(added);
            Childs = new List<Category>();
        }

        protected override async Task RollBackAsync()
        {
            if (_created.Count == 0)
            {
                return;
            }
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

        public async Task<ICollection<Category>> AllChildsAsync(Category? category, ICollection<Category>? result = null)
        {
            if(category is null)
            {
                throw new Exception("Finding childs for null category");
            }
            result ??= new List<Category>() { category!};
            var childs = await _repository.ListAsync(new CategoryChildsSpec(category!.Id));
            foreach (var child in childs)
            {
                result.Add(child);
                await AllChildsAsync(child, result);
            }
            return result;
        }

        public async Task<ICollection<Category>> CategoryTreeAsync(Category? category)
        {
            if(category is null)
            {
                return new List<Category>();
            }
            var tree = await GetParentTreeAsync(category);
            tree.Add(category);
            return tree;
        }
    }
}

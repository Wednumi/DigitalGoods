using Ardalis.Specification;
using DigitalGoods.Core.DbMethods;

namespace DigitalGoods.UnitTests.Mocks
{
    public class RepositoryMock<T> : IRepository<T> where T : class
    {
        private readonly List<T> _dataBase;

        private readonly Random _idRandomizer;

        public RepositoryMock()
        {
            _dataBase = new List<T>();
            _idRandomizer = new Random();
        }

        private void SetId(T entity)
        {
            //for future
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            SetId(entity);
            await Task.Run(() => _dataBase.Add(entity));
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                SetId(entity);
            }
            await Task.Run(() => _dataBase.AddRange(entities));
            return entities;
        }

        public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _dataBase.Count());
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => _dataBase.Remove(entity));
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await Task.Run(() => _dataBase.Remove(entity));
            }
        }

        public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            SetId(entity);
            await Task.Run(() => _dataBase.Add(entity));
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteProcedureAsync(ProcedureSpecification procedureSpec)
        {
            throw new NotImplementedException();
        }
    }
}

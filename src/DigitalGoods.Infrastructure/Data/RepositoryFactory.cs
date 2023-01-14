using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalGoods.Infrastructure.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DbContext _dbContext;

        public RepositoryFactory(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadRepository<T> CreateReadRepository<T>() where T : class
        {
            return (IReadRepository<T>)new Repository<T>(_dbContext);
        }

        public IRepository<T> CreateRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }
    }
}


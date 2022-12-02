using Ardalis.Specification.EntityFrameworkCore;
using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalGoods.Infrastructure.Data
{
    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

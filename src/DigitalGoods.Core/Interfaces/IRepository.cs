using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }

    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
    }
}

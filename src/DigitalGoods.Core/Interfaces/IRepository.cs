using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}

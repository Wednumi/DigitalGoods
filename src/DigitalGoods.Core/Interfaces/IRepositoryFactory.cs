using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IRepositoryFactory
    {
        public IRepository<T> CreateRepository<T>() where T : BaseEntity;

        public IReadRepository<T> CreateReadRepository<T>() where T : BaseEntity;
    }
}

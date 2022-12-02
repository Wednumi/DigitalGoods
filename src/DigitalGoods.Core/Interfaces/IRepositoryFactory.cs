namespace DigitalGoods.Core.Interfaces
{
    public interface IRepositoryFactory
    {
        public IRepository<T> CreateRepository<T>() where T : class;

        public IReadRepository<T> CreateReadRepository<T>() where T : class;
    }
}

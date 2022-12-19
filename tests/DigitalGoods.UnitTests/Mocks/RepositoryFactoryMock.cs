namespace DigitalGoods.UnitTests.Mocks
{
    public class RepositoryFactoryMock : IRepositoryFactory
    {
        public IReadRepository<T> CreateReadRepository<T>() where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public IRepository<T> CreateRepository<T>() where T : BaseEntity
        {
            return new RepositoryMock<T>();
        }
    }
}

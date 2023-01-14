namespace DigitalGoods.UnitTests.Mocks
{
    public class RepositoryFactoryMock : IRepositoryFactory
    {
        public IReadRepository<T> CreateReadRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IRepository<T> CreateRepository<T>() where T : class
        {
            return new RepositoryMock<T>();
        }
    }
}

using Ardalis.Specification;
using DigitalGoods.Core.DbMethods;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : BaseEntity
    {
        public Task ExecuteProcedureAsync(ProcedureSpecification procedureSpec);
    }

    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : BaseEntity
    {
    }
}

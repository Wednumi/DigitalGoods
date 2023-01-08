using Ardalis.Specification;
using DigitalGoods.Core.DbMethods;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
        public Task ExecuteProcedureAsync(ProcedureSpecification procedureSpec);

        public Task<FuncResult> ExecuteFunctionAsync<FuncResult>(FunctionSpecification<FuncResult> functionSpec);
    }

    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
    }
}

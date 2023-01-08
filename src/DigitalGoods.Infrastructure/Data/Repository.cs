using Ardalis.Specification.EntityFrameworkCore;
using DigitalGoods.Core.DbMethods;
using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalGoods.Infrastructure.Data
{
    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ExecuteProcedureAsync(ProcedureSpecification procedureSpec)
        {
            var name = procedureSpec.Procedure.Method.Name;
            await _dbContext.Database.ExecuteSqlAsync(procedureSpec.ExecQuery());
        }

        public async Task<FuncResult> ExecuteFunctionAsync<FuncResult>(FunctionSpecification<FuncResult> functionSpec)
        {
            var queryable = await Task.Run(() => _dbContext.Database.SqlQuery<FuncResult>(functionSpec.ExecQuery()));
            return queryable.ToList().First();
        }
    }
}

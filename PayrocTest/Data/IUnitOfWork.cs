using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PayrocTest.Data.Repositories;

namespace PayrocTest.Data
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : Entity<TId>;
        Task Commit();
    }
}
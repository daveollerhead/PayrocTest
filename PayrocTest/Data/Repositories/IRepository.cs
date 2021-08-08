using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace PayrocTest.Data.Repositories
{
    public interface IRepository<TEntity, in TId> where TEntity : Entity<TId>
    {
        Task<TEntity> Find(TId id);
        void Add(TEntity entity);
    }
}
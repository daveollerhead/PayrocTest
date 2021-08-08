using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace PayrocTest.Data.Repositories
{
    public class Repository<TEntity,  TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Find(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
    }
}
using System;
using System.Collections;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PayrocTest.Data.Repositories;

namespace PayrocTest.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Hashtable _repositories = new();

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : Entity<TId>
        {
            var entityType = typeof(TEntity);
            var key = entityType.Name;

            if (_repositories.Contains(key))
                return (IRepository<TEntity, TId>) _repositories[key];

            var repositoryType = typeof(Repository<TEntity, TId>);
            var instance = Activator.CreateInstance(repositoryType, _context);

            _repositories.Add(key, instance);

            return (IRepository<TEntity, TId>) instance;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
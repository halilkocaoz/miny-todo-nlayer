using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinyToDo.Abstract.Repositories;

namespace MinyToDo.Data.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected MinyToDoContext _context { get; private set; }
        private DbSet<TEntity> DbSet => _context.Set<TEntity>();
        public Repository(MinyToDoContext context)
        {
            _context = context;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0 ? entity : null;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _context.SaveChangesAsync() > 0 ? entity : null;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll() // todo: predicate
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
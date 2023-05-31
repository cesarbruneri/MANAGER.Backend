using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Sql.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MANAGER.Backend.Sql.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly ManagerContext _context;

        public BaseRepository(ManagerContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await FindByIdAsync(id);

            if (toDelete is not null)
            {
                _context.Set<TEntity>().Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TEntity?> FindByIdAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

        public async Task<List<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

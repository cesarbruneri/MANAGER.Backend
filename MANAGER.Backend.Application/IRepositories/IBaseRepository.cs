namespace MANAGER.Backend.Application.IRepositories;

public interface IBaseRepository<TEntity>
{
    Task<TEntity?> FindByIdAsync(int id);

    Task<List<TEntity>> GetAllAsync();

    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(int id);
}

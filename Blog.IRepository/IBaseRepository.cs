using SqlSugar;
using System.Linq.Expressions;

namespace Blog.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> EditAsync(TEntity entity);
        Task<TEntity> FindAsync(int id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);

        Task<List<TEntity>> QueryAsync();
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);
        Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total);
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total);
    }
}

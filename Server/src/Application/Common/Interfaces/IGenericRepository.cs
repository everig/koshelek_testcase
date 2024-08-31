namespace Application.Common.Interfaces
{
    public interface IGenericRepository<TEntity>    
    {
        Task Delete(int id);
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
    }
}

namespace SmallBookLibrary.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        Task<TEntity> Create(TEntity entity);
        Task Delete(Guid id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(Guid id);
        Task<TEntity> Update(TEntity entity);
    }
}
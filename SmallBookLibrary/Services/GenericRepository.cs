namespace SmallBookLibrary.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        private readonly AppDbContext _dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
           // var result = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entity.Id);
            return entity;
        }

        public async Task Delete(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var result= await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(e=>e.Id == id)
                .FirstOrDefaultAsync();

            if(result == null)
            {
                return null;
            }
            return result;
             
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;// await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entity.Id);
        }
    }
}

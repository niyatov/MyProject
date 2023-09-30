using MyProject.Data; // Using the data context for database operations

namespace MyProject.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        // Constructor to initialize GenericRepository with a data context
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        // Method to add a range of entities asynchronously
        public async ValueTask AddRangeAsync(IEnumerable<TEntity> TEntity)
        {
            await _context.Set<TEntity>().AddRangeAsync(TEntity);
            await _context.SaveChangesAsync();
        }

        // Method to get all entities of type TEntity
        public List<TEntity> GetAll()
            => _context.Set<TEntity>().ToList();

        // Method to update an entity asynchronously
        public async ValueTask<TEntity> Update(TEntity entity)
        {
            var entry = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        // Method to get an entity by its unique identifier asynchronously
        public ValueTask<TEntity?> GetByIdAsync(int id)
            => _context.Set<TEntity>().FindAsync(id);
    }
}

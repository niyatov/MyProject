namespace MyProject.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // Method to get all entities of type TEntity
        List<TEntity> GetAll();

        // Method to add a range of entities asynchronously
        ValueTask AddRangeAsync(IEnumerable<TEntity> TEntity);

        // Method to update an entity asynchronously
        ValueTask<TEntity> Update(TEntity entity);

        // Method to get an entity by its unique identifier asynchronously
        ValueTask<TEntity?> GetByIdAsync(int id);
    }
}

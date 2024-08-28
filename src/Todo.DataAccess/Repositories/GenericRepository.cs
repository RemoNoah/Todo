using Todo.DataAccess.Context;
using Todo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Todo.DataAccess.Repositories;

/// <summary>
/// Represents a generic repository for entities, implementing the <see cref="IGenericRepository{TEntity}" /> interface.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <exception cref="System.ArgumentNullException">Null exception.</exception>
    public GenericRepository(TodoContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DataSet = DbContext.Set<TEntity>();
    }

    /// <summary>
    /// Gets the database context.
    /// </summary>
    protected DbContext DbContext { get; }

    /// <summary>
    /// Gets the data set.
    /// </summary>
    protected DbSet<TEntity> DataSet { get; }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DataSet.FindAsync(id).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DataSet.ToListAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DataSet.Where(predicate).ToListAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public void Create(TEntity entity)
    {
        _ = DataSet.Add(entity);
    }

    /// <inheritdoc />
    public void CreateRange(IEnumerable<TEntity> entities)
    {
        DataSet.AddRange(entities);
    }

    /// <inheritdoc />
    public void Delete(TEntity entity)
    {
        _ = DataSet.Remove(entity);
    }

    /// <inheritdoc />
    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        DataSet.RemoveRange(entities);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetWithIncludesAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object?>>[] includes)
    {
        IQueryable<TEntity> query = DataSet.Where(expression);
        query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await DataSet.FirstOrDefaultAsync(expression, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object?>>[] includes)
    {
        IQueryable<TEntity> query = DataSet.Where(expression);
        query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object?>>[] includes)
    {
        IQueryable<TEntity> query = DataSet.Where(expression);
        query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await DataSet.Where(expression).ToListAsync();
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        _ = DataSet.Update(entity);
    }

    /// <inheritdoc />
    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        DataSet.UpdateRange(entities);
    }
}

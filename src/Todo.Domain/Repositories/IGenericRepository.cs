using System.Linq.Expressions;

namespace Todo.Domain.Repositories;

/// <summary>
/// Interface for <see cref="IGenericRepository<TEntity>"/> specific queries and methods.
/// </summary>
/// <typeparam name="TEntity">Type of the entity/class which this reposiotry targets.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Should return the entity with the matching id.
    /// </summary>
    /// <param name="id">Id of the entity which is requested.</param>
    Task<TEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Should return all entities hold by the repository.
    /// </summary>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Declare a GetAsync method.
    /// </summary>
    /// <param name="expression">Predicate</param>
    /// <param name="cancellationToken">CancellationToken</param>
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first entity matching the expression, related entities can be included.
    /// </summary>
    /// <param name="expression">Predicate</param>
    /// <param name="includes">Includes</param>
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object?>>[] includes);

    /// <summary>
    /// Declare a GetAsync method.
    /// </summary>
    /// <param name="expression">Predicate</param>
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Gets the first entity matching the expression, related entities can be included.
    /// </summary>
    /// <param name="expression">Predicate</param>
    /// <param name="includes">Includes</param>
    /// <returns>First or default</returns>
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object?>>[] includes);

    /// <summary>
    /// Should return all entities hold by the repository filtered by the provided <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">
    /// Predicate to filter / select certain entities who match the find criteria.
    /// </param>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets an entity from DbContext <see cref="IGenericRepository{T}.GetAsync(Expression{Func{T, bool}}, params Expression{{Func{T, object?}}[])"/>.
    /// </summary>
    /// <param name="expression">Predicate</param>
    /// <param name="includes">Include Func</param>
    Task<TEntity?> GetWithIncludesAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object?>>[] includes);

    /// <summary>
    /// Creates the provided <paramref name="entity"/> to the data set of the repository.
    /// </summary>
    /// <param name="entity">Entity to create.</param>
    void Create(TEntity entity);

    /// <summary>
    /// Creates the provided collection of entities to the data set of the repository.
    /// </summary>
    /// <param name="entities">Collection of entities to create.</param>
    void CreateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Declare a Put method.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Declare a UpdateRange method.
    /// </summary>
    /// <param name="entities">Collection of entities to update.</param>
    void UpdateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Should remove the provided <paramref name="entity"/> from the data set of the repository.
    /// </summary>
    /// <param name="entity">Entity to remove.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Should remove the provided collection of entities from the data set of the property.
    /// </summary>
    /// <param name="entities">Collection of entities to remove.</param>
    void DeleteRange(IEnumerable<TEntity> entities);
}

using Todo.Domain.Repositories;

namespace Todo.Domain.UnitOfWork;

/// <summary>
/// Interface for <see cref="IUnitOfWork"/>.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets User repository.
    /// </summary>
    public IUserRepository Users { get; }

    /// <summary>
    /// Gets Role repository.
    /// </summary>
    public IRoleRepository Roles { get; }

    /// <summary>
    /// Saves all changes, done within this unit of work
    /// </summary>
    /// <returns></returns>
    int Complete();

    /// <summary>
    /// Saves changes to the underlying db and completes the unit of work.
    /// </summary>
    /// <returns>The result is the number of state entries written to the db.</returns>
    Task<int> CompleteAsync();

    /// <summary>
    /// Checks if database connection can be established.
    /// </summary>
    /// <returns>If database connection can be established.</returns>
    Task<bool> CheckDatabaseConnectionStatus();
}
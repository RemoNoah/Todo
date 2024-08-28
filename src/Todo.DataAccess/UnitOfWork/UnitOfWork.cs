using Todo.DataAccess.Context;
using Todo.DataAccess.Repositories;
using Todo.Domain.Repositories;
using Todo.Domain.UnitOfWork;

namespace Todo.DataAccess.UnitOfWork;

/// <summary>
/// Represents a unit of work for coordinating database operations.
/// This class implements the <see cref="IUnitOfWork"/> interface.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly TodoContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="currentUserService">The current user service.</param>
    /// <exception cref="System.ArgumentNullException">context</exception>
    public UnitOfWork(TodoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Users = new UserRepository(context);
        Roles = new RoleRepository(context);
    }

    /// <inheritdoc />
    public IUserRepository Users { get; }

    /// <inheritdoc />
    public IRoleRepository Roles { get; }

    /// <inheritdoc />
    public int Complete()
    {
        return _context.SaveChanges();
    }

    /// <inheritdoc />
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<bool> CheckDatabaseConnectionStatus()
    {
        return await _context.Database.CanConnectAsync().ConfigureAwait(false);
    }
}
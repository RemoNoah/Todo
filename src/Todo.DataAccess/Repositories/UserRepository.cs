using Todo.DataAccess.Context;
using Todo.Domain.Models;
using Todo.Domain.Repositories;

namespace Todo.DataAccess.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="User"/> entities, implementing the <see cref="IUserRepository"/> interface.
/// </summary>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UserRepository(TodoContext dbContext) : base(dbContext) { }
}

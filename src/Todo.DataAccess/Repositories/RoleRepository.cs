using Todo.DataAccess.Context;
using Todo.Domain.Models;
using Todo.Domain.Repositories;

namespace Todo.DataAccess.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="Role"/> entities, implementing the <see cref="IRoleRepository"/> interface.
/// </summary>
public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRepository" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RoleRepository(TodoContext dbContext) : base(dbContext) { }
}

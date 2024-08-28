using Todo.Domain.Models;

namespace Todo.Domain.Repositories;

/// <summary>
/// Interface for <see cref="IUserRepository"/> specific queries and methods.
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
}

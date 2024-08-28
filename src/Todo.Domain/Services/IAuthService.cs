using Todo.Domain.DTO.User;
using Todo.Domain.Models;

namespace Todo.Domain.Services;

/// <summary>
/// Interface for <see cref="IAuthService"/>.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Logs in the User
    /// </summary>
    /// <param name="userLoginDto"> the User login DTO</param>
    /// <returns>The User</returns>
    Task<User?> LoginAsync(UserLoginDTO userLoginDto);

    /// <summary>
    /// Register a User
    /// </summary>
    /// <param name="user"> the User Registration DTO</param>
    /// <returns> The User></returns>
    Task<User?> RegisterAsync(UserRegistrationDTO user);
}

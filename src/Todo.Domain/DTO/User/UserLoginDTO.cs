namespace Todo.Domain.DTO.User;

/// <summary>
/// Represents a Data Transfer Object (DTO) for an UserLoginDTO.
/// </summary>
public class UserLoginDTO
{
    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = null!;
}

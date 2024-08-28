namespace Todo.Domain.Models;

/// <summary>
/// Represents an Role entity
/// </summary>
public class Role
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Users.
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = null!;
}

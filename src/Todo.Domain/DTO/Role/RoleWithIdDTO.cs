namespace Todo.Domain.DTO.Role;

/// <summary>
/// Represents a Data Transfer Object (DTO) for an RoleWithIdDTO.
/// </summary>
public class RoleWithIdDTO
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    public string Name { get; set; } = null!;
}

namespace Todo.Domain.DTO.Role;

/// <summary>
/// Represents a Data Transfer Object (DTO) for an RoleUpdateByOldNameDTO.
/// </summary>
public class RoleUpdateByOldNameDTO
{
    /// <summary>
    /// Gets or sets the old name.
    /// </summary>
    public string OldName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the new name.
    /// </summary>
    public string NewName { get; set; } = null!;
}

using Todo.Domain.DTO.Role;

namespace Todo.Domain.Services;

/// <summary>
/// Interface for <see cref="IRoleService"/>.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Gets all roles without the Ids of the Roles.
    /// </summary>
    /// <returns>All roles in a IEnumerable</returns>
    Task<IEnumerable<RoleWithoutIdDTO>> GetAllWithoutId();

    /// <summary>
    /// Gets all roles with the Ids of the Roles.
    /// </summary>
    /// <returns>All roles in a IEnumerable</returns>
    Task<IEnumerable<RoleWithIdDTO>> GetAllWithId();

    /// <summary>
    /// Gets the Id by the Name of the Role.
    /// </summary>
    /// <param name="name">The Name of the Role</param>
    /// <returns>The Id of the role with the given Name</returns>
    Task<Guid> GetIdByName(string name);

    /// <summary>
    /// Gets the Name of the role by the Name.
    /// </summary>
    /// <param name="id">The Id of the Role</param>
    /// <returns>The Name of the role with the given Id</returns>
    Task<string> GetNameById(Guid id);

    /// <summary>
    /// Updates the name of a specified role based on the given role Id.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The updated Role</returns>
    Task<RoleWithoutIdDTO> UpdateById(RoleWithIdDTO role);

    /// <summary>
    /// Updates the name of a specified role based on the old name.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The updated Role</returns>
    Task<RoleWithoutIdDTO> UpdateByOldName(RoleUpdateByOldNameDTO role);

    /// <summary>
    /// Creates the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The created Role</returns>
    Task<RoleWithoutIdDTO> Create(RoleWithoutIdDTO role);

    /// <summary>
    /// Delete the specified role by the given Id.
    /// </summary>
    /// <param name="roleId">The id of the role.</param>
    /// <returns>A bool, true if the Role was removed, else false</returns>
    Task<bool> DeleteById(Guid roleId);

    /// <summary>
    /// Delete the specified role by the given Name.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <returns>A bool, true if the Role was removed, else false</returns>
    Task<bool> DeleteByName(string name);
}

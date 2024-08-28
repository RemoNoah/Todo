using AutoMapper;
using Todo.Domain.DTO.Role;
using Todo.Domain.Models;
using Todo.Domain.Services;
using Todo.Domain.UnitOfWork;
using System.Data;

namespace Todo.Services.Services;

/// <summary>
/// Service class for managing user-related operations.
/// This class implements the <see cref="IAuthService"/> interface.
/// </summary>
/// <param name="unitOfWork"> <see cref="UnitOfWork"/> </param>
public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc/>
    public async Task<IEnumerable<RoleWithoutIdDTO>> GetAllWithoutId()
    {
        IEnumerable<RoleWithoutIdDTO> roles = (await _unitOfWork.Roles.GetAllAsync())
            .Select(_mapper.Map<RoleWithoutIdDTO>);
        return roles;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RoleWithIdDTO>> GetAllWithId()
    {
        IEnumerable<RoleWithIdDTO> roles = (await _unitOfWork.Roles.GetAllAsync())
            .Select(_mapper.Map<RoleWithIdDTO>);
        return roles;
    }

    /// <inheritdoc/>
    public async Task<Guid> GetIdByName(string name)
    {
        Role? role = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == name);
        return role!.Id;
    }

    /// <inheritdoc/>
    public async Task<string> GetNameById(Guid id)
    {
        Role? role = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Id == id);
        return role!.Name;
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> Create(RoleWithoutIdDTO role)
    {
        Role? existingRole = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == role.Name);

        if (existingRole != null)
        {
            return new();
        }

        Role newRole = _mapper.Map<Role>(role);
        newRole.Id = Guid.NewGuid();

        _unitOfWork.Roles.Create(newRole);
        _ = await _unitOfWork.CompleteAsync();
        return _mapper.Map<RoleWithoutIdDTO>(newRole);
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> UpdateById(RoleWithIdDTO role)
    {
        Role? existingRole = await _unitOfWork.Roles.GetByIdAsync(role.Id);

        if (existingRole == null)
        {
            return new();
        }

        Role updatedRole = _mapper.Map<Role>(role);

        _unitOfWork.Roles.Update(updatedRole);
        _ = await _unitOfWork.CompleteAsync();

        return _mapper.Map<RoleWithoutIdDTO>(updatedRole);
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> UpdateByOldName(RoleUpdateByOldNameDTO role)
    {
        Role? existingRole = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == role.OldName);

        if (existingRole == null)
        {
            return new();
        }

        Role updatedRole = _mapper.Map<Role>(role);
        updatedRole.Id = existingRole.Id;

        _unitOfWork.Roles.Update(updatedRole);
        _ = await _unitOfWork.CompleteAsync();

        return _mapper.Map<RoleWithoutIdDTO>(updatedRole);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteById(Guid roleId)
    {
        Role? existingRole = await _unitOfWork.Roles.GetByIdAsync(roleId);
        if (existingRole != null)
        {
            _unitOfWork.Roles.Delete(existingRole);
            _ = await _unitOfWork.CompleteAsync();
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteByName(string name)
    {
        Role? existingRole = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == name);
        if (existingRole != null)
        {
            _unitOfWork.Roles.Delete(existingRole);
            _ = await _unitOfWork.CompleteAsync();
            return true;
        }

        return false;
    }
}

using Todo.Api.Auth;
using Todo.Domain.DTO.Role;
using Todo.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<IEnumerable<RoleWithoutIdDTO>>> GetAllWithoutId()
    {
        return Ok((await _roleService.GetAllWithoutId()).ToList());
    }

    [HttpGet]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<IEnumerable<RoleWithoutIdDTO>>> GetAllWithId()
    {
        return Ok((await _roleService.GetAllWithId()).ToList());
    }

    [HttpGet]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<Guid>> GetIdByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Please enter a Name");
        }

        Guid result = await _roleService.GetIdByName(name);

        return result == Guid.Empty ? (ActionResult<Guid>)BadRequest("Role with this Name was not found") : (ActionResult<Guid>)Ok(result);
    }

    [HttpGet]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<string>> GetNameById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Please enter a Id");
        }

        string result = await _roleService.GetNameById(id);

        return string.IsNullOrWhiteSpace(result) ? (ActionResult<string>)BadRequest("Role with this Id was not found") : (ActionResult<string>)Ok(result);
    }

    [HttpPost]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<RoleWithoutIdDTO>> Create(RoleWithoutIdDTO role)
    {
        if (role == null || string.IsNullOrWhiteSpace(role.Name))
        {
            return BadRequest("Please enter a RoleWithoutIdDTO");
        }

        RoleWithoutIdDTO result = await _roleService.Create(role);

        return string.IsNullOrWhiteSpace(result.Name) ? (ActionResult<RoleWithoutIdDTO>)BadRequest("Role can not be Created") : (ActionResult<RoleWithoutIdDTO>)Ok(result);
    }

    [HttpPut]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<RoleWithoutIdDTO>> UpdateById(RoleWithIdDTO role)
    {
        if (role == null || string.IsNullOrWhiteSpace(role.Name))
        {
            return BadRequest("Please enter a RoleWithIdDTO");
        }

        RoleWithoutIdDTO result = await _roleService.UpdateById(role);

        return string.IsNullOrWhiteSpace(result.Name) ? (ActionResult<RoleWithoutIdDTO>)BadRequest("Role can not be Updated") : (ActionResult<RoleWithoutIdDTO>)Ok(result);
    }

    [HttpPut]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<RoleWithoutIdDTO>> UpdateByOldName(RoleUpdateByOldNameDTO role)
    {
        if (role == null || string.IsNullOrWhiteSpace(role.OldName) || string.IsNullOrWhiteSpace(role.NewName))
        {
            return BadRequest("Please enter a RoleUpdateByOldNameDTO");
        }

        RoleWithoutIdDTO result = await _roleService.UpdateByOldName(role);

        return string.IsNullOrWhiteSpace(result.Name) ? (ActionResult<RoleWithoutIdDTO>)BadRequest("Role can not be Updated") : (ActionResult<RoleWithoutIdDTO>)Ok(result);
    }

    [HttpDelete]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<bool>> DeleteById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Please enter a id");
        }

        bool result = await _roleService.DeleteById(id);

        return !result ? (ActionResult<bool>)BadRequest("Role was not found") : (ActionResult<bool>)Ok("Role has been deleted");
    }

    [HttpDelete]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult<bool>> DeleteByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Please enter a name");
        }

        bool result = await _roleService.DeleteByName(name);

        return !result ? (ActionResult<bool>)BadRequest("Role was not found") : (ActionResult<bool>)Ok("Role has been deleted");
    }
}

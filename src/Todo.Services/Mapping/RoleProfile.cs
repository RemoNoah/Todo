using AutoMapper;
using Todo.Domain.DTO.Role;
using Todo.Domain.Models;

namespace Todo.Services.Mapping;

/// <summary>
/// Initializes a new instance of the <see cref="RoleProfile"/> class.
/// </summary>
public class RoleProfile : Profile
{
    /// <summary>
    /// Configures the AutoMapper mapping from 
    /// <see cref="Role"/> to <see cref="RoleWithIdDTO"/>,
    /// <see cref="Role"/> to <see cref="RoleWithoutIdDTO"/>.
    /// <see cref="RoleWithoutIdDTO"/> to <see cref="Role"/>.
    /// <see cref="RoleWithIdDTO"/> to <see cref="Role"/>.
    /// <see cref="RoleUpdateByOldNameDTO"/> to <see cref="Role"/>.
    /// </summary>
    public RoleProfile()
    {
        _ = CreateMap<Role, RoleWithIdDTO>();

        _ = CreateMap<Role, RoleWithoutIdDTO>();

        _ = CreateMap<RoleWithoutIdDTO, Role>();

        _ = CreateMap<RoleWithIdDTO, Role>();

        _ = CreateMap<RoleUpdateByOldNameDTO, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NewName));
    }
}

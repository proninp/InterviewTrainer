using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Roles;

public record RoleDto(long Id, string Name, string? Description);

public static class RoleDtoExtension
{
    public static RoleDto ToDto(this Role role) =>
        new RoleDto(role.Id, role.Name, role.Description);
}
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.DTOs.Roles;

public record CreateRoleDto(string Name, string? Description);

public static class CreateRoleDtoExtension
{
    public static Role ToRole(this CreateRoleDto role) =>
        new(role.Name, role.Description);
}
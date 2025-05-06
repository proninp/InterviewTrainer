namespace InterviewTrainer.Application.Contracts.Roles;

public record RoleFilterDto(int ItemsPerPage, int Page, string? Name = null, string? Description = null);
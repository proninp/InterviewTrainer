namespace InterviewTrainer.Application.DTOs.Roles;

public record RoleFilterDto(int ItemsPerPage, int Page, string? Name, string? Description);
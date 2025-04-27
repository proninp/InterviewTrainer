namespace InterviewTrainer.Application.DTOs.Technologies;

public record TechnologyFilterDto(int ItemsPerPage, int Page, string? Name = null, bool Archived = false);
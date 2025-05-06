namespace InterviewTrainer.Application.Contracts.Technologies;

public record TechnologyFilterDto(int ItemsPerPage, int Page, string? Name = null, bool? Archived = null);
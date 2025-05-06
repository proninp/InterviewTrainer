namespace InterviewTrainer.Application.Contracts.Tags;

public record TagFilterDto(int ItemsPerPage, int Page, string? Name = null);
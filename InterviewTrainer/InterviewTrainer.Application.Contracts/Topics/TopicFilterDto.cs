namespace InterviewTrainer.Application.Contracts.Topics;

public record TopicFilterDto(int ItemsPerPage, int Page, string? Name = null, bool? Archived = null);
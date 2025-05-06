namespace InterviewTrainer.Application.Contracts.Topics;

public record UpdateTopicDto(Guid Id, string? Name = null, bool? Archived = false);
namespace InterviewTrainer.Application.Contracts.Topics;

public record UpdateTopicDto(long Id, string? Name = null, bool? Archived = false);
namespace InterviewTrainer.Application.DTOs.Topics;

public record UpdateTopicDto(Guid Id, string? Name = null, bool? Archived = false);
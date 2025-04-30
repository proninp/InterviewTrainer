namespace InterviewTrainer.Application.DTOs.Topic;

public record UpdateTopicDto(Guid Id, string? Name = null, bool Archived = false);
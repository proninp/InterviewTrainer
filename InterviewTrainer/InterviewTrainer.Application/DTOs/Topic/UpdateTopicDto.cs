namespace InterviewTrainer.Application.DTOs.Topic;

public record UpdateTopicDto(Guid Id, string Name, bool Archived = false);
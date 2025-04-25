namespace InterviewTrainer.Application.DTOs.Topic;

public record TopicFilterDto(int ItemsPerPage, int Page, string? Name = null, bool Archived = false);
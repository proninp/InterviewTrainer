namespace InterviewTrainer.Application.DTOs.Topics;

public record TopicFilterDto(int ItemsPerPage, int Page, string? Name = null, bool? Archived = null);
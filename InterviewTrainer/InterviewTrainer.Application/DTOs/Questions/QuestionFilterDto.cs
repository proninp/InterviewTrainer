using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.DTOs.Questions;

public record QuestionFilterDto(
    int ItemsPerPage,
    int Page,
    Guid? TopicId = null,
    Difficulty? Difficulty = null,
    QuestionStatus? Status = null,
    string? Text = null,
    bool? Archived = null,
    bool? IsAnswered = null);
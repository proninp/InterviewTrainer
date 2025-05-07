using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.Contracts.Questions;

public record QuestionFilterDto(
    int ItemsPerPage,
    int Page,
    long? TopicId = null,
    Difficulty? Difficulty = null,
    QuestionStatus? Status = null,
    string? Text = null,
    bool? Archived = null,
    bool? IsAnswered = null);
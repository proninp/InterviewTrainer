namespace InterviewTrainer.Application.DTOs.SuggestedAnswers;

public record SuggestedAnswerFilterDto(
    int ItemsPerPage,
    int Page,
    Guid? QuestionId = null,
    string? Answer = null);
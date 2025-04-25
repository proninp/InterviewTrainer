namespace InterviewTrainer.Application.DTOs.SuggestedAnswers;

public record SuggestedAnswerFilterDto(
    int ItemsPerPage,
    int Page,
    Guid? SuggestedQuestionId = null,
    string? Answer = null);
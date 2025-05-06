namespace InterviewTrainer.Application.Contracts.SuggestedAnswers;

public record SuggestedAnswerFilterDto(
    int ItemsPerPage,
    int Page,
    Guid? QuestionId = null,
    string? Answer = null);
namespace InterviewTrainer.Application.Contracts.SuggestedAnswers;

public record SuggestedAnswerFilterDto(
    int ItemsPerPage,
    int Page,
    long? QuestionId = null,
    string? Answer = null);
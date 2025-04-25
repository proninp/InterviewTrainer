namespace InterviewTrainer.Application.DTOs.SuggestedQuestions;

public record SuggestedQuestionFilterDto(
    int ItemsPerPage,
    int Page,
    Guid? TopicId = null,
    Guid? DifficultyId = null,
    string? Text = null);
namespace InterviewTrainer.Application.DTOs.Questions;

public record QuestionFilterDto(
    int ItemsPerPage,
    int Page,
    Guid? TopicId = null,
    Guid? DifficultyId = null,
    string? Text = null,
    bool Archived = false);
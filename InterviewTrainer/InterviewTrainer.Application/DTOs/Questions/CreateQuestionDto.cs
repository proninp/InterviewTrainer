using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.DTOs.Questions;

public record CreateQuestionDto(
    Guid TopicId,
    Difficulty Difficulty,
    string Text,
    string? Answer = null,
    QuestionStatus Status = QuestionStatus.New,
    bool Archived = false);
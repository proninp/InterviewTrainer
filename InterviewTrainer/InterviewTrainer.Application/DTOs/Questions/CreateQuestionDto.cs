using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.DTOs.Questions;

public record CreateQuestionDto(
    Guid TopicId, Difficulty Difficulty, QuestionStatus Status, string Text, string Answer);
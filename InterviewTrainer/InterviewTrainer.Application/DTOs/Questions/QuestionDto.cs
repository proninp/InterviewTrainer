using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.DTOs.Questions;

public record QuestionDto(
    Guid TopicId, Difficulty Difficulty, QuestionStatus Status, string Text, string Answer, bool Archived);
namespace InterviewTrainer.Application.DTOs.Questions;

public record QuestionDto(Guid TopicId, Guid DifficultyId, string Text, string Answer, bool Archived);
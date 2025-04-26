namespace InterviewTrainer.Application.DTOs.Questions;

public record CreateQuestionDto(Guid TopicId, Guid DifficultyId, string Text, string Answer);
namespace InterviewTrainer.Application.DTOs.SuggestedAnswers;

public record SuggestedAnswerDto(Guid Id, Guid QuestionId, string Answer);
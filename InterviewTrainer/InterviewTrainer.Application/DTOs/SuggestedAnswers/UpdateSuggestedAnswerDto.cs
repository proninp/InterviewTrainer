namespace InterviewTrainer.Application.DTOs.SuggestedAnswers;

public record UpdateSuggestedAnswerDto(Guid Id, Guid? QuestionId, string Answer);
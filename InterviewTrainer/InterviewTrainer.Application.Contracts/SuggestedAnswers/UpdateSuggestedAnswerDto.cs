namespace InterviewTrainer.Application.Contracts.SuggestedAnswers;

public record UpdateSuggestedAnswerDto(Guid Id, Guid? QuestionId, string Answer);
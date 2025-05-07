namespace InterviewTrainer.Application.Contracts.SuggestedAnswers;

public record UpdateSuggestedAnswerDto(long Id, long? QuestionId, string Answer);
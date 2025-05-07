using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.SuggestedAnswers;

public record SuggestedAnswerDto(long Id, long QuestionId, string Answer);

public static class SuggestedAnswerDtoExtension
{
    public static SuggestedAnswerDto ToDto(this SuggestedAnswer suggestedAnswer) =>
        new(suggestedAnswer.Id, suggestedAnswer.QuestionId, suggestedAnswer.Answer);
}
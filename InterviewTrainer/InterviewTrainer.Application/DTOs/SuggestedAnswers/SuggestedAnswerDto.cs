using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.DTOs.SuggestedAnswers;

public record SuggestedAnswerDto(Guid Id, Guid QuestionId, string Answer);

public static class SuggestedAnswerDtoExtension
{
    public static SuggestedAnswerDto ToDto(this SuggestedAnswer suggestedAnswer) =>
        new(suggestedAnswer.Id, suggestedAnswer.QuestionId, suggestedAnswer.Answer);
}
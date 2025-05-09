﻿using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.SuggestedAnswers;

public record CreateSuggestedAnswerDto(long QuestionId, string Answer);

public static class CreateSuggestedAnswerDtoExtension
{
    public static SuggestedAnswer ToSuggestedAnswer(this CreateSuggestedAnswerDto createSuggestedAnswerDto) =>
        new(createSuggestedAnswerDto.QuestionId, createSuggestedAnswerDto.Answer);
}
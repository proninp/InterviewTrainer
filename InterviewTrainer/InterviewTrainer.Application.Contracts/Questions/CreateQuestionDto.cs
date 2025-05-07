using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Application.Contracts.Questions;

public record CreateQuestionDto(
    long TopicId,
    Difficulty Difficulty,
    string Text,
    string? Answer = null,
    bool Archived = false)
{
    public QuestionStatus Status { get; set; } = QuestionStatus.New;
}

public static class CreateQuestionDtoExtension
{
    public static Question ToQuestion(this CreateQuestionDto createQuestionDto) =>
        new(createQuestionDto.TopicId, createQuestionDto.Difficulty, createQuestionDto.Status,
            createQuestionDto.Text, createQuestionDto.Answer, createQuestionDto.Archived);
}
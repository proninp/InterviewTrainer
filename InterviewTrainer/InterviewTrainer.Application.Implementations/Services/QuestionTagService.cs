using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Questions;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Services;

public class QuestionTagService : IQuestionTagService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public QuestionTagService(IQuestionRepository questionRepository, ITagRepository tagRepository,
        IUnitOfWork unitOfWork)
    {
        _questionRepository = questionRepository;
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<QuestionDto>> GetQuestionsByTagNameAsync(string tagName, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetQuestionsByTagNameAsync(tagName, cancellationToken);
        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<Result<QuestionDto>> AddTagToQuestionAsync(long questionId, long tagId,
        CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsync(questionId, cancellationToken);
        
        if (question is null)
            return Result.Fail<QuestionDto>(ErrorsFactory.NotFound(nameof(question), questionId));
        
        if (question.QuestionTags.Any(t => t.TagId == tagId))
        {
            return Result.Ok(question.ToDto());
        }

        var tag = await _tagRepository.GetAsync(tagId, cancellationToken);
        if (tag is null)
            return Result.Fail<QuestionDto>(ErrorsFactory.NotFound(nameof(tag), tagId));

        var questionTag = new QuestionTag(questionId, tagId);

        question.QuestionTags.Add(questionTag);
        _questionRepository.Update(question);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(question.ToDto());
    }

    public async Task<Result<QuestionDto>> RemoveTagFromQuestionAsync(long questionId, long tagId,
        CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsync(questionId, cancellationToken);
        if (question is null)
            return Result.Fail<QuestionDto>(ErrorsFactory.NotFound(nameof(question), questionId));
        
        var questionTag = question.QuestionTags.FirstOrDefault(qt => qt.TagId == tagId);

        if (questionTag is null)
        {
            return Result.Ok(question.ToDto());
        }

        question.QuestionTags.Remove(questionTag);
        _questionRepository.Update(question);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(question.ToDto());
    }
}
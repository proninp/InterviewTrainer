using InterviewTrainer.Application.Contracts.Repositories;
using InterviewTrainer.Application.Contracts.Services;
using InterviewTrainer.Application.DTOs.Questions;
using InterviewTrainer.Domain.Entities;

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

    public async Task<QuestionDto> AddTagToQuestionAsync(Guid questionId, Guid tagId,
        CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetOrThrowAsync(questionId, cancellationToken);
        if (question.QuestionTags.Any(t => t.TagId == tagId))
        {
            return question.ToDto();
        }

        _ = await _tagRepository.GetOrThrowAsync(tagId, cancellationToken);

        var questionTag = new QuestionTag(questionId, tagId);

        question.QuestionTags.Add(questionTag);
        _questionRepository.Update(question);
        await _unitOfWork.CommitAsync(cancellationToken);

        return question.ToDto();
    }

    public async Task<QuestionDto> RemoveTagFromQuestionAsync(Guid questionId, Guid tagId,
        CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetOrThrowAsync(questionId, cancellationToken);
        var questionTag = question.QuestionTags.FirstOrDefault(qt => qt.TagId == tagId);

        if (questionTag is null)
        {
            return question.ToDto();
        }

        question.QuestionTags.Remove(questionTag);
        _questionRepository.Update(question);
        await _unitOfWork.CommitAsync(cancellationToken);

        return question.ToDto();
    }
}
using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Questions;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    {
        _questionRepository = questionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<QuestionDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var question =
            await _questionRepository.GetAsync(id, cancellationToken, asNoTracking: true);
        return question is null
            ? Result.Fail<QuestionDto>(ErrorsFactory.NotFound(nameof(question), id))
            : Result.Ok(question.ToDto());
    }

    public async Task<List<QuestionDto>> GetPagedAsync(QuestionFilterDto questionFilterDto,
        CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetPagedAsync(questionFilterDto, cancellationToken);
        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<Result<QuestionDto>> GetRandomAsync(QuestionFilterDto questionFilterDto,
        CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetPagedAsync(questionFilterDto, cancellationToken);
        var questionsList = questions.ToList();

        if (questionsList.Count == 0)
        {
            return Result.Fail<QuestionDto>(QuestionErrors.NoQuestionsFoundByFilter());
        }

        var random = new Random();
        var question = questionsList[random.Next(0, questionsList.Count)];

        return Result.Ok(question.ToDto());
    }

    public async Task<Result<QuestionDto>> CreateAsync(CreateQuestionDto createQuestionDto,
        CancellationToken cancellationToken)
    {
        var checkResult = CheckQuestionIdentityPropertiesAsync(
            null, createQuestionDto.Text, createQuestionDto.Answer);
        if (checkResult.IsFailed)
            return Result.Fail<QuestionDto>(checkResult.Errors);

        var question = await _questionRepository.AddAsync(createQuestionDto.ToQuestion(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(question.ToDto());
    }

    public async Task<Result> UpdateAsync(UpdateQuestionDto updateQuestionDto, CancellationToken cancellationToken)
    {
        var checkResult = CheckQuestionIdentityPropertiesAsync(updateQuestionDto.Id, updateQuestionDto.Text,
            updateQuestionDto.Answer);

        if (checkResult.IsFailed)
            return checkResult;

        var question = await _questionRepository.GetAsync(updateQuestionDto.Id, cancellationToken);

        if (question is null)
            return Result.Fail(ErrorsFactory.NotFound(nameof(question), updateQuestionDto.Id));

        var isNeedUpdate = false;

        if (updateQuestionDto.TopicId is not null && question.TopicId != updateQuestionDto.TopicId.Value)
        {
            
            question.TopicId = updateQuestionDto.TopicId.Value;
            isNeedUpdate = true;
        }

        if (updateQuestionDto.Difficulty is not null && question.Difficulty != updateQuestionDto.Difficulty.Value)
        {
            question.Difficulty = updateQuestionDto.Difficulty.Value;
            isNeedUpdate = true;
        }

        if (updateQuestionDto.Status is not null && question.Status != updateQuestionDto.Status.Value)
        {
            question.Status = updateQuestionDto.Status.Value;
            isNeedUpdate = true;
        }

        if (updateQuestionDto.Text is not null && !string.Equals(updateQuestionDto.Text, updateQuestionDto.Text,
                StringComparison.InvariantCultureIgnoreCase))
        {
            question.Text = updateQuestionDto.Text;
            isNeedUpdate = true;
        }

        if (updateQuestionDto.Answer is not null && !string.Equals(updateQuestionDto.Answer, updateQuestionDto.Answer,
                StringComparison.InvariantCultureIgnoreCase))
        {
            question.Answer = updateQuestionDto.Answer;
            isNeedUpdate = true;
        }

        if (updateQuestionDto.Archived is not null && updateQuestionDto.Archived.Value != question.Archived)
        {
            question.Archived = updateQuestionDto.Archived.Value;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _questionRepository.Update(question);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        return Result.Ok();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _questionRepository.TryDeleteAsync(id, cancellationToken);
    }

    private Result CheckQuestionIdentityPropertiesAsync(long? excludeId, string? text, string? answer)
    {
        if (text is not null && string.IsNullOrWhiteSpace(text))
        {
            return Result.Fail(ErrorsFactory.Required(nameof(Question), nameof(text)));
        }

        return Result.Ok();
    }
}
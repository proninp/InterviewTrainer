using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.SuggestedAnswers;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Implementations.Services;

public class SuggestedAnswerService : ISuggestedAnswerService
{
    private readonly ISuggestedAnswerRepository _suggestedAnswerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SuggestedAnswerService(ISuggestedAnswerRepository suggestedAnswerRepository,
        IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    {
        _suggestedAnswerRepository = suggestedAnswerRepository;
        _questionRepository = questionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SuggestedAnswerDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var suggestedAnswer = await _suggestedAnswerRepository.GetAsync(id, cancellationToken, disableTracking: true);
        return suggestedAnswer is null
            ? Result.Fail<SuggestedAnswerDto>(ErrorsFactory.NotFound(nameof(suggestedAnswer), id))
            : Result.Ok(suggestedAnswer.ToDto());
    }

    public async Task<List<SuggestedAnswerDto>> GetPagedAsync(SuggestedAnswerFilterDto suggestedAnswerFilterDto,
        CancellationToken cancellationToken)
    {
        var suggestedAnswers =
            await _suggestedAnswerRepository.GetPagedAsync(suggestedAnswerFilterDto, cancellationToken);
        return suggestedAnswers.Select(sa => sa.ToDto()).ToList();
    }

    public async Task<Result<SuggestedAnswerDto>> CreateAsync(CreateSuggestedAnswerDto createSuggestedAnswerDto,
        CancellationToken cancellationToken)
    {
        var checkResult = await CheckQuestionExists(createSuggestedAnswerDto.QuestionId, cancellationToken);
        if (checkResult.IsFailed)
            return Result.Fail<SuggestedAnswerDto>(checkResult.Errors);
        
        var sa = await _suggestedAnswerRepository.AddAsync(createSuggestedAnswerDto.ToSuggestedAnswer(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(sa.ToDto());
    }

    public async Task<Result> UpdateAsync(UpdateSuggestedAnswerDto updateSuggestedAnswerDto, CancellationToken cancellationToken)
    {
        if (updateSuggestedAnswerDto.QuestionId is not null)
        { 
            var checkResult = await CheckQuestionExists(updateSuggestedAnswerDto.QuestionId.Value, cancellationToken);
            if (checkResult.IsFailed)
                return checkResult;
        }
        
        var suggestedAnswer = await _suggestedAnswerRepository.GetAsync(updateSuggestedAnswerDto.Id, cancellationToken);
        if (suggestedAnswer is null)
            return Result.Fail(ErrorsFactory.NotFound(nameof(suggestedAnswer), updateSuggestedAnswerDto.Id));
        
        var isNeedUpdate = false;
        
        if (!string.Equals(suggestedAnswer.Answer, updateSuggestedAnswerDto.Answer))
        {
            suggestedAnswer.Answer = updateSuggestedAnswerDto.Answer;
            isNeedUpdate = true;
        }

        if (updateSuggestedAnswerDto.QuestionId is not null &&
            suggestedAnswer.QuestionId != updateSuggestedAnswerDto.QuestionId.Value)
        {
            suggestedAnswer.QuestionId = updateSuggestedAnswerDto.QuestionId.Value;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _suggestedAnswerRepository.Update(suggestedAnswer);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        return Result.Ok();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        _suggestedAnswerRepository.Delete(id);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<Result> CheckQuestionExists(long questionId, CancellationToken cancellationToken)
    {
        var isQuestionExists = await _questionRepository.AnyAsync(questionId, cancellationToken);
        if (!isQuestionExists)
            Result.Fail<SuggestedAnswerDto>(ErrorsFactory.NotFound(nameof(Question), questionId));
        return Result.Ok();
    }
}
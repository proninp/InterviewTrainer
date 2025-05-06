using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.SuggestedAnswers;

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

    public async Task<SuggestedAnswerDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var sa = await _suggestedAnswerRepository.GetOrThrowAsync(id, cancellationToken);
        return sa.ToDto();
    }

    public async Task<List<SuggestedAnswerDto>> GetPagedAsync(SuggestedAnswerFilterDto suggestedAnswerFilterDto,
        CancellationToken cancellationToken)
    {
        var suggestedAnswers =
            await _suggestedAnswerRepository.GetPagedAsync(suggestedAnswerFilterDto, cancellationToken);
        return suggestedAnswers.Select(sa => sa.ToDto()).ToList();
    }

    public async Task<SuggestedAnswerDto> CreateAsync(CreateSuggestedAnswerDto createSuggestedAnswerDto,
        CancellationToken cancellationToken)
    {
        _ = await _questionRepository.GetOrThrowAsync(createSuggestedAnswerDto.QuestionId, cancellationToken);
        
        var sa = await _suggestedAnswerRepository.AddAsync(createSuggestedAnswerDto.ToSuggestedAnswer(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return sa.ToDto();
    }

    public async Task UpdateAsync(UpdateSuggestedAnswerDto updateSuggestedAnswerDto, CancellationToken cancellationToken)
    {
        if (updateSuggestedAnswerDto.QuestionId is not null)
        { 
            _ = await _questionRepository.GetOrThrowAsync(updateSuggestedAnswerDto.QuestionId.Value, cancellationToken);
        }
        
        var sa = await _suggestedAnswerRepository.GetOrThrowAsync(updateSuggestedAnswerDto.Id, cancellationToken);

        var isNeedUpdate = false;
        
        if (!string.Equals(sa.Answer, updateSuggestedAnswerDto.Answer))
        {
            sa.Answer = updateSuggestedAnswerDto.Answer;
            isNeedUpdate = true;
        }

        if (updateSuggestedAnswerDto.QuestionId is not null &&
            sa.QuestionId != updateSuggestedAnswerDto.QuestionId.Value)
        {
            sa.QuestionId = updateSuggestedAnswerDto.QuestionId.Value;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _suggestedAnswerRepository.Update(sa);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sa = await _suggestedAnswerRepository.GetAsync(id, cancellationToken);
        if (sa is not null)
        {
            _suggestedAnswerRepository.Delete(sa);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
using InterviewTrainer.Application.Contracts.Repositories;
using InterviewTrainer.Application.Contracts.Services;
using InterviewTrainer.Application.DTOs.Questions;
using InterviewTrainer.Application.Exceptions;

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

    public async Task<QuestionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetOrThrowAsync(id, cancellationToken);
        return question.ToDto();
    }

    public async Task<List<QuestionDto>> GetPagedAsync(QuestionFilterDto questionFilterDto,
        CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetPagedAsync(questionFilterDto, cancellationToken);
        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<QuestionDto> GetRandomAsync(QuestionFilterDto questionFilterDto,
        CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetPagedAsync(questionFilterDto, cancellationToken);
        var questionsList = questions.ToList();

        if (questionsList.Count == 0)
        {
            throw new BusinessRuleViolationException("Not a single question was found for the selected parameters.");
        }

        var random = new Random();
        var question = questionsList[random.Next(0, questionsList.Count)];

        return question.ToDto();
    }

    public async Task<QuestionDto> CreateAsync(CreateQuestionDto createQuestionDto, CancellationToken cancellationToken)
    {
        await CheckQuestionIdentityPropertiesAsync(null, createQuestionDto.Text, createQuestionDto.Answer,
            cancellationToken);

        var question = await _questionRepository.AddAsync(createQuestionDto.ToQuestion(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return question.ToDto();
    }

    public async Task UpdateAsync(UpdateQuestionDto updateQuestionDto, CancellationToken cancellationToken)
    {
        await CheckQuestionIdentityPropertiesAsync(updateQuestionDto.Id, updateQuestionDto.Text,
            updateQuestionDto.Answer,
            cancellationToken);

        var question = await _questionRepository.GetOrThrowAsync(updateQuestionDto.Id, cancellationToken);

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
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsync(id, cancellationToken);
        if (question is not null)
        {
            _questionRepository.Delete(question);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    private async Task CheckQuestionIdentityPropertiesAsync(Guid? excludeId, string? text, string? answer,
        CancellationToken cancellationToken)
    {
        if (text is not null && string.IsNullOrWhiteSpace(text))
        {
            throw new BusinessRuleViolationException("Question text cannot be empty.");
        }
    }
}
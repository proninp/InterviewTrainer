using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Topics;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Services;

public class TopicService : ITopicService
{
    private readonly ITopicRepository _topicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TopicService(ITopicRepository topicRepository, IUnitOfWork unitOfWork)
    {
        _topicRepository = topicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TopicDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var topic = await _topicRepository.GetAsync(id, cancellationToken);
        return topic is null 
            ? Result.Fail<TopicDto>(ErrorsFactory.NotFound(nameof(topic), id)) 
            : Result.Ok(topic.ToDto());
    }

    public async Task<List<TopicDto>> GetPagedAsync(TopicFilterDto topicFilterDto, CancellationToken cancellationToken)
    {
        var topics = await _topicRepository.GetPagedAsync(topicFilterDto, cancellationToken);
        return topics.Select(t => t.ToDto()).ToList();
    }

    public async Task<Result<TopicDto>> CreateAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken)
    {
        var checkResult = await CheckTopicIdentityPropertiesAsync(null, createTopicDto.Name, cancellationToken);
        if (checkResult.IsFailed)
            return Result.Fail<TopicDto>(checkResult.Errors);

        var topic = await _topicRepository.AddAsync(createTopicDto.ToTopic(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(topic.ToDto());
    }

    public async Task<Result> UpdateAsync(UpdateTopicDto updateTopicDto, CancellationToken cancellationToken)
    {
        var checkResult = await CheckTopicIdentityPropertiesAsync(updateTopicDto.Id, updateTopicDto.Name, cancellationToken);
        if (checkResult.IsFailed)
            return checkResult;
        
        var topic = await _topicRepository.GetAsync(updateTopicDto.Id, cancellationToken);
        if (topic is null)
            return Result.Fail(ErrorsFactory.NotFound(nameof(topic), updateTopicDto.Id));
        
        var isNeedUpdate = false;
        
        if (updateTopicDto.Name is not null &&
            !string.Equals(updateTopicDto.Name, topic.Name, StringComparison.OrdinalIgnoreCase))
        {
            topic.Name = updateTopicDto.Name;
            isNeedUpdate = true;
        }

        if (updateTopicDto.Archived is not null && updateTopicDto.Archived.Value != topic.Archived)
        {
            topic.Archived = updateTopicDto.Archived.Value;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _topicRepository.Update(topic);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        return Result.Ok();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _topicRepository.TryDeleteAsync(id, cancellationToken);
    }

    private async Task<Result> CheckTopicIdentityPropertiesAsync(long? excludeId, string? name,
        CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail(ErrorsFactory.Required(nameof(Topic), nameof(name)));
            
            var isTopicAlreadyExists = await _topicRepository.ExistsByNameAsync(name, excludeId, cancellationToken);
            if (isTopicAlreadyExists)
            {
                return Result.Fail(ErrorsFactory.AlreadyExists(nameof(Topic), nameof(name), name));
            }
        }
        return Result.Ok();
    }
}
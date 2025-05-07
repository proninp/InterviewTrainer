using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Topics;
using InterviewTrainer.Application.Implementations.Exceptions;

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

    public async Task<TopicDto> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var topic = await _topicRepository.GetOrThrowAsync(id, cancellationToken);
        return topic.ToDto();
    }

    public async Task<List<TopicDto>> GetPagedAsync(TopicFilterDto topicFilterDto, CancellationToken cancellationToken)
    {
        var topics = await _topicRepository.GetPagedAsync(topicFilterDto, cancellationToken);
        return topics.Select(t => t.ToDto()).ToList();
    }

    public async Task<TopicDto> CreateAsync(CreateTopicDto createTopicDto, CancellationToken cancellationToken)
    {
        await CheckTopicIdentityPropertiesAsync(null, createTopicDto.Name, cancellationToken);

        var topic = await _topicRepository.AddAsync(createTopicDto.ToTopic(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return topic.ToDto();
    }

    public async Task UpdateAsync(UpdateTopicDto updateTopicDto, CancellationToken cancellationToken)
    {
        await CheckTopicIdentityPropertiesAsync(updateTopicDto.Id, updateTopicDto.Name, cancellationToken);

        var isNeedUpdate = false;
        var topic = await _topicRepository.GetOrThrowAsync(updateTopicDto.Id, cancellationToken);
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
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var topic = await _topicRepository.GetAsync(id, cancellationToken);
        if (topic is not null)
        {
            _topicRepository.Delete(topic);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    private async Task CheckTopicIdentityPropertiesAsync(long? excludeId, string? name,
        CancellationToken cancellationToken)
    {
        if (name is not null)
        {
            var isTopicAlreadyExists = await _topicRepository.ExistsByNameAsync(name, excludeId, cancellationToken);
            if (isTopicAlreadyExists)
            {
                throw new BusinessRuleViolationException($"Topic with Name '{name}' already exists");
            }
        }
    }
}
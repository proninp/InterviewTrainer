using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Technologies;
using InterviewTrainer.Application.Contracts.Topics;

namespace InterviewTrainer.Application.Implementations.Services;

public class TopicTechnologyService : ITopicTechnologyService
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly ITopicRepository _topicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TopicTechnologyService(ITechnologyRepository technologyRepository, ITopicRepository topicRepository,
        IUnitOfWork unitOfWork)
    {
        _technologyRepository = technologyRepository;
        _topicRepository = topicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TopicDto>> GetTopicsByTechnologyNameAsync(string technologyName,
        CancellationToken cancellationToken)
    {
        var topics = await _topicRepository.GetTopicsByTechnologyNameAsync(technologyName, cancellationToken);
        return topics.Select(t => t.ToDto()).ToList();
    }

    public async Task<TechnologyDto> AddTopicAsync(long technologyId, long topicId, CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetOrThrowAsync(technologyId, cancellationToken);

        if (technology.TopicTechnologies.Any(tt => tt.TopicId == topicId))
        {
            return technology.ToDto();
        }

        _ = await _topicRepository.GetOrThrowAsync(topicId, cancellationToken);

        var topicTechnology = new TopicTechnology(technologyId, topicId);

        technology.TopicTechnologies.Add(topicTechnology);
        _technologyRepository.Update(technology);
        await _unitOfWork.CommitAsync(cancellationToken);

        return technology.ToDto();
    }

    public async Task<TechnologyDto> RemoveTopicAsync(long technologyId, long topicId,
        CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetOrThrowAsync(technologyId, cancellationToken);
        var topicTechnology = technology.TopicTechnologies.FirstOrDefault(tt => tt.TopicId == topicId);

        if (topicTechnology is null)
        {
            return technology.ToDto();
        }

        technology.TopicTechnologies.Remove(topicTechnology);
        _technologyRepository.Update(technology);
        await _unitOfWork.CommitAsync(cancellationToken);

        return technology.ToDto();
    }
}
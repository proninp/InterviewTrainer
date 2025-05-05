using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.DTOs.Topics;

public record CreateTopicDto(string Name, bool Archived = false);

public static class CreateTopicDtoExtension
{
    public static Topic ToTopic(this CreateTopicDto createTopicDto) =>
        new Topic(createTopicDto.Name, createTopicDto.Archived);
    
}
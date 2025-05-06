using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Tags;

public record CreateTagDto(string Name);

public static class CreateTagDtoExtension
{
    public static Tag ToTag(this CreateTagDto createTagDto) =>
        new(createTagDto.Name);
}
using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.DTOs.Technologies;

public record CreateTechnologyDto(string Name, bool Archived = false);

public static class CreateTechnologyDtoExtension
{
    public static Technology ToTechnology(this CreateTechnologyDto createTechnologyDto) =>
        new(createTechnologyDto.Name, createTechnologyDto.Archived);
}
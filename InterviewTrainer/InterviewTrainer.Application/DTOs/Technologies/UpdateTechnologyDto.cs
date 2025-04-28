namespace InterviewTrainer.Application.DTOs.Technologies;

public record UpdateTechnologyDto(Guid Id, string Name, bool Archived = false);
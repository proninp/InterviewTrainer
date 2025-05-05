namespace InterviewTrainer.Application.DTOs.Technologies;

public record UpdateTechnologyDto(Guid Id, string? Name = null, bool? Archived = null);
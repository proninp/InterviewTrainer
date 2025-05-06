namespace InterviewTrainer.Application.Contracts.Technologies;

public record UpdateTechnologyDto(Guid Id, string? Name = null, bool? Archived = null);
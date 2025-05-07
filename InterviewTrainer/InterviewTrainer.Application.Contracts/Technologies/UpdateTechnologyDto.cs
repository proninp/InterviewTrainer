namespace InterviewTrainer.Application.Contracts.Technologies;

public record UpdateTechnologyDto(long Id, string? Name = null, bool? Archived = null);
namespace InterviewTrainer.Application.DTOs.Tags;

public record TagFilterDto(int ItemsPerPage, int Page, string? Name = null);
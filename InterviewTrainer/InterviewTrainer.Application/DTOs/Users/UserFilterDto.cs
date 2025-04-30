namespace InterviewTrainer.Application.DTOs.Users;

public record UserFilterDto(
    int ItemsPerPage, int Page, long? TelegramId = null, string? UserName = null, string? Email = null);
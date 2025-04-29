namespace InterviewTrainer.Application.DTOs.Users;

public record UpdateUserDto(Guid Id, long? TelegramId = null, string? UserName = null, string? Email = null);
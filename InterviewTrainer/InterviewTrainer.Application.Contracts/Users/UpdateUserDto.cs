namespace InterviewTrainer.Application.Contracts.Users;

public record UpdateUserDto(long Id, long? TelegramId, string? UserName = null, string? Email = null);
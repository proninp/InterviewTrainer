namespace InterviewTrainer.Application.Contracts.Users;

public record UpdateUserDto(Guid Id, long? TelegramId, string? UserName = null, string? Email = null);
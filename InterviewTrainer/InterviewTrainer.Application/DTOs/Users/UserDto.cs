namespace InterviewTrainer.Application.DTOs.Users;

public record UserDto(Guid Id, long TelegramId, string? UserName, string? Email, List<Guid> RoleIds);
namespace InterviewTrainer.Application.DTOs.Users;

public record CreateUserDto(long TelegramId, string? UserName = null, string? Email = null)
{
    public List<Guid> RoleIds { get; init; } = [];
}
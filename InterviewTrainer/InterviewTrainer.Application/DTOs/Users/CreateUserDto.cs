namespace InterviewTrainer.Application.DTOs.Users;

public record CreateUserDto
{
    public long TelegramId { get; init; }
    
    public string? UserName { get; init; }
    
    public string? Email {get; init; }
    
    public List<Guid> RoleIds { get; init; } = [];

    public CreateUserDto(long telegramId, string? userName = null, string? email = null)
    {
        TelegramId = telegramId;
        UserName = userName;
        Email = email;
    }
}
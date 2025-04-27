namespace InterviewTrainer.Application.DTOs.Users;

public record UpdateUserDto
{
    public Guid Id { get; init; }
    
    public long TelegramId { get; init; }
    
    public string? UserName { get; init; }
    
    public string? Email {get; init; }
    
    public List<Guid> RoleIds { get; init; } = [];
    
    public UpdateUserDto(Guid id, long telegramId, string? userName = null, string? email = null)
    {
        Id = id;
        TelegramId = telegramId;
        UserName = userName;
        Email = email;
    }
}
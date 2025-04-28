namespace InterviewTrainer.Application.DTOs.Users;

public record UserDto
{
    public Guid Id { get; init; }
    
    public long TelegramId { get; init; }
    
    public string? UserName { get; init; }
    
    public string? Email { get; init; }
    
    public List<Guid> RoleIds { get; init; } = [];
}
    
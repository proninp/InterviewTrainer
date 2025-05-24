using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class User
    : IdentityModel
{
    public long? TelegramId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public ICollection<UserRole> UserRoles { get; init; } = new List<UserRole>();
    
    public User() { }

    public User(long? telegramId, string? userName = null, string? email = null)
    {
        TelegramId = telegramId;
        UserName = userName;
        Email = email;
    }
}
using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class User(long? telegramId, string? userName = null, string? email = null)
    : IdentityModel
{
    public long? TelegramId { get; set; } = telegramId;

    public string? UserName { get; set; } = userName;

    public string? Email { get; set; } = email;

    public ICollection<UserRole> UserRoles { get; init; } = new List<UserRole>();
}
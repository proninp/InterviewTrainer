using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class UserRole(long userId, long roleId)
{
    public long UserId { get; set; } = userId;

    public User User { get; set; } = null!;

    public long RoleId { get; set; } = roleId;

    public Role Role { get; set; } = null!;
}
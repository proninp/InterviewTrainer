using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class UserRole(Guid userId, Guid roleId) : IdentityModel
{
    public Guid UserId { get; set; } = userId;

    public User User { get; set; } = null!;

    public Guid RoleId { get; set; } = roleId;

    public Role Role { get; set; } = null!;
}
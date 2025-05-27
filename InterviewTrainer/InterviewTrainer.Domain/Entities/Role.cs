using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Role(string name, string? description = null) : IdentityModel
{
    public string Name { get; set; } = name;

    public string? Description { get; set; } = description;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
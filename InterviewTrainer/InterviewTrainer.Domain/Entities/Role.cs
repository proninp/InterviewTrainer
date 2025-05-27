using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Role : IdentityModel
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public Role() {  }

    public Role(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }
}
using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Topic(string name, bool archived = false) : IdentityModel
{
    public string Name { get; set; } = name;

    public bool Archived { get; set; } = archived;
}
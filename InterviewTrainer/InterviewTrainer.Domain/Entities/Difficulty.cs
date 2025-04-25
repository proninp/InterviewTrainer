using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Difficulty(string name) : IdentityModel
{
    public string Name { get; set; } = name;
}
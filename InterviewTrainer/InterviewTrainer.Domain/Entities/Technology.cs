using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Technology : IdentityModel
{
    public string Name { get; set; } = null!;

    public bool Archived { get; set; }
    
    public ICollection<TopicTechnology> TopicTechnologies { get; set; } = new List<TopicTechnology>();

    public Technology()
    {

    }

    public Technology(string name, bool archived = false)
    {
        Name = name;
        Archived = archived;
    }
}
using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Tag : IdentityModel
{
    public string Name { get; set; }
    
    public ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();
    
    public Tag() { }

    public Tag(string name)
    {
        Name = name;
    }
}
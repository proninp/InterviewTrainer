using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Tag(string name) : IdentityModel
{
    public string Name { get; set; } = name;
    
    public ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();
}
using InterviewTrainer.Domain.Abstractions;

namespace InterviewTrainer.Domain.Entities;

public class Tag(string name) : IdentityModel
{
    public string Name { get; set; } = name;
    
    public ICollection<QuestionTags> Tags { get; set; } = new List<QuestionTags>();
}
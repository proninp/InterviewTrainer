﻿using InterviewTrainer.Domain.Abstractions;
using InterviewTrainer.Domain.Enums;

namespace InterviewTrainer.Domain.Entities;

public class Question(
    long topicId, long authorId, Difficulty difficulty, QuestionStatus status, string text, string? answer, bool archived = false)
    : IdentityModel
{
    public long TopicId { get; set; } = topicId;

    public Topic Topic { get; set; } = null!;
    
    public long AuthorId { get; set; } = authorId;
    
    public User Author { get; set; } = null!;

    public Difficulty Difficulty { get; set; } = difficulty;

    public QuestionStatus Status { get; set; } = status;

    public string Text { get; set; } = text;

    public string? Answer { get; set; } = answer;

    public bool Archived { get; set; } = archived;
    
    public ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();
}
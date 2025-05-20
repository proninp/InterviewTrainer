using InterviewTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTrainer.Infrastructure.EntityFramework.Configurations;

public class QuestionTagConfiguration : IEntityTypeConfiguration<QuestionTag>
{
    public void Configure(EntityTypeBuilder<QuestionTag> builder)
    {
        builder.HasKey(qt => new { qt.QuestionId, qt.TagId });

        builder
            .HasOne(qt => qt.Question)
            .WithMany(q => q.QuestionTags);
        
        builder
            .HasOne(qt => qt.Tag)
            .WithMany(t => t.QuestionTags);
    }
}
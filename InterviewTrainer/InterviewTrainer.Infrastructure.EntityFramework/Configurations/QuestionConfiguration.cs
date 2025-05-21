using InterviewTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTrainer.Infrastructure.EntityFramework.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);
        builder.HasIndex(q => q.Difficulty);
        builder.Property(q => q.Text).IsRequired();
        builder.HasIndex(q => q.Text);

        builder
            .HasOne(q => q.Topic)
            .WithMany()
            .HasForeignKey(q => q.TopicId)
            .IsRequired();

        builder
            .HasOne(q => q.Author)
            .WithMany();
        
        builder
            .HasMany(q => q.QuestionTags)
            .WithOne(qt => qt.Question);
        
    }
}
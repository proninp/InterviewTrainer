using InterviewTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTrainer.Infrastructure.EntityFramework.Configurations;

public class TopicTechnologiesConfiguration : IEntityTypeConfiguration<TopicTechnology>
{
    public void Configure(EntityTypeBuilder<TopicTechnology> builder)
    {
        builder.HasKey(tt => new { tt.TechnologyId, tt.TopicId });
        builder
            .HasOne(tt => tt.Technology)
            .WithMany(t => t.TopicTechnologies);
        
        builder
            .HasOne(tt => tt.Topic)
            .WithMany(t => t.TopicTechnologies);
    }
}
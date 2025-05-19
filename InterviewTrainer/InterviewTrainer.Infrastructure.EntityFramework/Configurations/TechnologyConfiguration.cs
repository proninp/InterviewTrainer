using InterviewTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTrainer.Infrastructure.EntityFramework.Configurations;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired();
        builder
            .HasMany(t => t.TopicTechnologies)
            .WithOne(tt => tt.Technology);
    }
}
using InterviewTrainer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTrainer.Infrastructure.EntityFramework.Configurations;

public class SuggestedAnswerConfiguration : IEntityTypeConfiguration<SuggestedAnswer>
{
    public void Configure(EntityTypeBuilder<SuggestedAnswer> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Answer).IsRequired();

        builder
            .HasOne(s => s.Question)
            .WithMany()
            .HasForeignKey(s => s.QuestionId);
    }
}
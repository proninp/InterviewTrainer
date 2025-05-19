using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Infrastructure.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InterviewTrainer.Infrastructure.EntityFramework;

public class DatabaseContext : DbContext, IUnitOfWork
{
    private readonly DbSettings _options;

    protected DatabaseContext(IOptionsSnapshot<DbSettings> options)
    {
        _options = options.Value;
    }
    
    #region DbSets
    
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }
    
    public DbSet<UserRole> UserRoles { get; set; }
    
    public DbSet<Technology> Technologies { get; set; }
    
    public DbSet<Topic> Topics { get; set; }
    
    public DbSet<TopicTechnology> TopicTechnologies { get; set; }
    
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<Tag> Tags { get; set; }
    
    public DbSet<QuestionTag> QuestionTags { get; set; }
    
    public DbSet<SuggestedAnswer> SuggestedAnswers { get; set; }
    
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_options.DbConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await SaveChangesAsync(cancellationToken);
}
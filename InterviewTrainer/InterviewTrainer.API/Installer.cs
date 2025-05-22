using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Implementations.Services;
using InterviewTrainer.Infrastructure.Repositories;

namespace InterviewTrainer.API;

public static class Installer
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .InstallServices()
            .InstallRepositories();
        
        return services;
    }
    
    private static IServiceCollection InstallServices(this IServiceCollection services)
    {
        services
            .AddTransient<IUserService, UserService>()
            .AddTransient<IRoleService, RoleService>()
            .AddTransient<IUserRoleService, UserRoleService>()
            .AddTransient<ITechnologyService, TechnologyService>()
            .AddTransient<ITopicService, TopicService>()
            .AddTransient<ITopicTechnologyService, TopicTechnologyService>()
            .AddTransient<IQuestionService, QuestionService>()
            .AddTransient<ITagService, TagService>()
            .AddTransient<IQuestionTagService, QuestionTagService>()
            .AddTransient<ISuggestedAnswerService, SuggestedAnswerService>();
        
        return services;
    }
    
    private static IServiceCollection InstallRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<ITechnologyRepository, TechnologyRepository>();
        return services;
    }
}
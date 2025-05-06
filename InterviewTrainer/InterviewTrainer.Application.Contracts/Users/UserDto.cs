using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.Contracts.Users;

public record UserDto(Guid Id, long? TelegramId, string? UserName, string? Email)
{
    public List<Guid> RoleIds { get; init; } = [];
}

public static class UserDtoExtension
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto(user.Id, user.TelegramId, user.UserName, user.Email)
        {
            RoleIds = user.UserRoles.Select(ur => ur.RoleId).ToList()
        };
    }
}
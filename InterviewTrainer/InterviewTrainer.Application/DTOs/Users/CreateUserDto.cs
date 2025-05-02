using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.DTOs.Users;

public record CreateUserDto(long TelegramId, string? UserName = null, string? Email = null)
{
    public List<Guid> RoleIds { get; init; } = [];
}

public static class CreateUserDtoExtension
{
    public static User ToUser(this CreateUserDto createUserDto) =>
        new User(createUserDto.TelegramId, createUserDto.UserName, createUserDto.Email);
}
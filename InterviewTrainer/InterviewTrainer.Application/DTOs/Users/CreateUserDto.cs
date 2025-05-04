using InterviewTrainer.Domain.Entities;

namespace InterviewTrainer.Application.DTOs.Users;

public record CreateUserDto(long? TelegramId, string? UserName = null, string? Email = null);

public static class CreateUserDtoExtension
{
    public static User ToUser(this CreateUserDto createUserDto) =>
        new(createUserDto.TelegramId, createUserDto.UserName, createUserDto.Email);
}
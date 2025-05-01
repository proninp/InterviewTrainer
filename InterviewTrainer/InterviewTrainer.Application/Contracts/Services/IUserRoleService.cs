using InterviewTrainer.Application.DTOs.Users;

namespace InterviewTrainer.Application.Contracts.Services;

public interface IUserRoleService
{
    Task<bool> CheckUserRoleExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    
    Task<UserDto?> AddUserRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    
    Task<UserDto?> RemoveUserRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
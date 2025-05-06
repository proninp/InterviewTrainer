using InterviewTrainer.Application.Contracts.Users;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IUserRoleService
{
    Task<bool> CheckUserRoleExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    
    Task<List<UserDto>> GetUsersByRollNameAsync(string roleName, CancellationToken cancellationToken);
    
    Task<UserDto> AddUserRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    
    Task<UserDto> RemoveUserRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
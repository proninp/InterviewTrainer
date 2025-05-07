using InterviewTrainer.Application.Contracts.Users;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IUserRoleService
{
    Task<bool> CheckUserRoleExistsAsync(long userId, long roleId, CancellationToken cancellationToken);
    
    Task<List<UserDto>> GetUsersByRollNameAsync(string roleName, CancellationToken cancellationToken);
    
    Task<UserDto> AddUserRoleAsync(long userId, long roleId, CancellationToken cancellationToken);
    
    Task<UserDto> RemoveUserRoleAsync(long userId, long roleId, CancellationToken cancellationToken);
}
using InterviewTrainer.Application.Contracts.Users;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IUserRoleService
{
    Task<Result<bool>> CheckUserRoleExistsAsync(long userId, long roleId, CancellationToken cancellationToken);
    
    Task<List<UserDto>> GetUsersByRollNameAsync(string roleName, CancellationToken cancellationToken);
    
    Task<Result<UserDto>> AddUserRoleAsync(long userId, long roleId, CancellationToken cancellationToken);
    
    Task<Result<UserDto>> RemoveUserRoleAsync(long userId, long roleId, CancellationToken cancellationToken);
}
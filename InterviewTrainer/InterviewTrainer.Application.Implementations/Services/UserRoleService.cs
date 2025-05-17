using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Users;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserRoleService(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> CheckUserRoleExistsAsync(long userId, long roleId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(userId, cancellationToken, asNoTracking: true);
        return user is null 
            ? Result.Fail<bool>(ErrorsFactory.NotFound(nameof(user), userId))
            : Result.Ok(user.UserRoles.Any(ur => ur.RoleId == roleId));
    }

    public async Task<List<UserDto>> GetUsersByRollNameAsync(string roleName, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersByRoleNameAsync(roleName, cancellationToken);
        return users.Select(u => u.ToDto()).ToList();
    }

    public async Task<Result<UserDto>> AddUserRoleAsync(long userId, long roleId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(userId, cancellationToken);
        if (user is null)
            return Result.Fail<UserDto>(ErrorsFactory.NotFound(nameof(user), userId));

        if (user.UserRoles.Any(ur => ur.RoleId == roleId))
        {
            return Result.Ok(user.ToDto());
        }

        var isRoleExists = await _roleRepository.AnyAsync(roleId, cancellationToken);
        if (!isRoleExists)
            return Result.Fail(ErrorsFactory.NotFound(nameof(isRoleExists), roleId));
        
        var userRole = new UserRole(userId, roleId);
        
        user.UserRoles.Add(userRole);
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Ok(user.ToDto());
    }

    public async Task<Result<UserDto>> RemoveUserRoleAsync(long userId, long roleId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(userId, cancellationToken);
        if (user is null)
            return Result.Fail<UserDto>(ErrorsFactory.NotFound(nameof(user), userId));
        
        var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);
        if (userRole is null)
        {
            return Result.Ok(user.ToDto());
        }

        user.UserRoles.Remove(userRole);
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Ok(user.ToDto());
    }
}
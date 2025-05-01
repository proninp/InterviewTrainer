using InterviewTrainer.Application.Contracts.Repositories;
using InterviewTrainer.Application.Contracts.Services;
using InterviewTrainer.Application.DTOs.Users;
using InterviewTrainer.Domain.Entities;

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

    public async Task<bool> CheckUserRoleExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetOrThrowAsync(userId, cancellationToken);
        return user.UserRoles.Any(ur => ur.RoleId == roleId);
    }

    public async Task<UserDto?> AddUserRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetOrThrowAsync(userId, cancellationToken);

        if (user.UserRoles.Any(ur => ur.RoleId == roleId))
            return user.ToDto();

        _ = await _roleRepository.GetOrThrowAsync(roleId, cancellationToken);
        
        var userRole = new UserRole(userId, roleId);
        
        user.UserRoles.Add(userRole);
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();
        
        return user.ToDto();
    }

    public async Task<UserDto?> RemoveUserRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetOrThrowAsync(userId, cancellationToken);

        var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);
        if (userRole is null)
            return user.ToDto();

        user.UserRoles.Remove(userRole);
        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();
        
        return user.ToDto();
    }
}
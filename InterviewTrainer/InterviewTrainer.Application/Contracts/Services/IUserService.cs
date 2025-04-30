using InterviewTrainer.Application.DTOs.Users;

namespace InterviewTrainer.Application.Contracts.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<UserDto>> GetPagedAsync(UserFilterDto userFilterDto, CancellationToken cancellationToken);
    
    Task<bool> CheckRoleExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    
    Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    Task<UserDto?> AddRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    
    Task<UserDto?> RemoveRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
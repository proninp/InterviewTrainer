using InterviewTrainer.Application.Contracts.Users;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto, CancellationToken cancellationToken);
    
    Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
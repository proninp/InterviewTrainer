using InterviewTrainer.Application.Contracts.Users;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto, CancellationToken cancellationToken);
    
    Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    
    Task UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}
using InterviewTrainer.Application.Contracts.Users;
using FluentResults;

namespace InterviewTrainer.Application.Abstractions.Services;

public interface IUserService
{
    Task<Result<UserDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto, CancellationToken cancellationToken);
    
    Task<Result<UserDto>> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    
    Task<Result> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}
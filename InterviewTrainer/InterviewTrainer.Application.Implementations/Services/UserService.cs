using InterviewTrainer.Domain.Entities;
using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Application.Abstractions.Services;
using InterviewTrainer.Application.Contracts.Users;
using InterviewTrainer.Application.Implementations.Errors;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(id, cancellationToken, asNoTracking: true);
        return user is null
            ? Result.Fail<UserDto>(ErrorsFactory.NotFound(nameof(user), id))
            : Result.Ok(user.ToDto());
    }

    public async Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetPagedAsync(userFilterDto, cancellationToken);
        return users.Select(user => user.ToDto()).ToList();
    }

    public async Task<Result<UserDto>> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var checkResult =
            await CheckUserIdentityPropertiesAsync(null, createUserDto.TelegramId, createUserDto.Email,
                cancellationToken);
        if (checkResult.IsFailed)
            return Result.Fail<UserDto>(checkResult.Errors);

        var user = await _userRepository.AddAsync(createUserDto.ToUser(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(user.ToDto());
    }

    public async Task<Result> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(updateUserDto.Id, cancellationToken);
        
        if (user is null)
            return Result.Fail(ErrorsFactory.NotFound(nameof(user), updateUserDto.Id));
        
        var checkResult = 
            await CheckUserIdentityPropertiesAsync(updateUserDto.Id, updateUserDto.TelegramId, updateUserDto.Email,
            cancellationToken);
        
        if (checkResult.IsFailed)
            return checkResult;
        
        var isNeedUpdate = false;

        if (updateUserDto.TelegramId is not null && updateUserDto.TelegramId != user.TelegramId)
        {
            user.TelegramId = updateUserDto.TelegramId.Value;
            isNeedUpdate = true;
        }

        if (updateUserDto.UserName is not null &&
            !string.Equals(updateUserDto.UserName, user.UserName, StringComparison.OrdinalIgnoreCase))
        {
            user.UserName = updateUserDto.UserName;
            isNeedUpdate = true;
        }

        if (updateUserDto.Email is not null &&
            !string.Equals(updateUserDto.Email, user.Email, StringComparison.OrdinalIgnoreCase))
        {
            user.Email = updateUserDto.Email;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        
        return Result.Ok();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _userRepository.TryDeleteAsync(id, cancellationToken);
    }

    private async Task<Result> CheckUserIdentityPropertiesAsync(long? excludeId, long? telegramId,
        string? email,
        CancellationToken cancellationToken)
    {
        bool isUserAlreadyExists;

        if (telegramId is not null)
        {
            isUserAlreadyExists =
                await _userRepository.ExistsByTelegramIdAsync(telegramId.Value, excludeId, cancellationToken);
            if (isUserAlreadyExists)
            {
                return Result.Fail(ErrorsFactory.AlreadyExists(nameof(User), nameof(telegramId), telegramId.Value));
            }
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            isUserAlreadyExists =
                await _userRepository.ExistsByEmailAsync(email, excludeId, cancellationToken);
            if (isUserAlreadyExists)
            {
                return Result.Fail(ErrorsFactory.AlreadyExists(nameof(User), nameof(email), email));
            }
        }
        return Result.Ok();
    }
}
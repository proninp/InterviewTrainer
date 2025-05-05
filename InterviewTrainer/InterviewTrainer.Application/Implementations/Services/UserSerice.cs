using InterviewTrainer.Application.Contracts.Repositories;
using InterviewTrainer.Application.Contracts.Services;
using InterviewTrainer.Application.DTOs.Users;
using InterviewTrainer.Application.Exceptions;

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

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetOrThrowAsync(id, cancellationToken);
        return user.ToDto();
    }

    public async Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetPagedAsync(userFilterDto, cancellationToken);
        return users.Select(user => user.ToDto()).ToList();
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        await CheckUserIdentityPropertiesAsync(null, createUserDto.TelegramId, createUserDto.Email, cancellationToken);

        var user = await _userRepository.AddAsync(createUserDto.ToUser(), cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return user.ToDto();
    }

    public async Task UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        await CheckUserIdentityPropertiesAsync(updateUserDto.Id, updateUserDto.TelegramId, updateUserDto.Email,
            cancellationToken);

        var user = await _userRepository.GetOrThrowAsync(updateUserDto.Id, cancellationToken);

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
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(id, cancellationToken);
        if (user is not null)
        {
            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }

    private async Task CheckUserIdentityPropertiesAsync(Guid? excludeId, long? telegramId, string? email,
        CancellationToken cancellationToken)
    {
        bool isUserAlreadyExists;

        if (telegramId is not null)
        {
            isUserAlreadyExists =
                await _userRepository.ExistsByTelegramIdAsync(telegramId.Value, excludeId, cancellationToken);
            if (isUserAlreadyExists)
            {
                throw new BusinessRuleViolationException(
                    $"User with Telegram ID '{telegramId}' already exists");
            }
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            isUserAlreadyExists =
                await _userRepository.ExistsByEmailAsync(email, excludeId, cancellationToken);
            if (isUserAlreadyExists)
            {
                throw new BusinessRuleViolationException(
                    $"A user account with the specified Email '{email}' already exists");
            }
        }
    }
}
using InterviewTrainer.Application.Contracts.Repositories;
using InterviewTrainer.Application.Contracts.Services;
using InterviewTrainer.Application.DTOs.Users;

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
        var user = createUserDto.ToUser();
        var userDto = (await _userRepository.AddAsync(user, cancellationToken)).ToDto();
        return userDto;
    }

    public async Task UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetOrThrowAsync(updateUserDto.Id, cancellationToken);

        var isNeedUpdate = false;

        if (updateUserDto.TelegramId is not null && updateUserDto.TelegramId != user.TelegramId)
        {
            user.TelegramId = updateUserDto.TelegramId.Value;
            isNeedUpdate = true;
        }

        if (updateUserDto.UserName is not null && updateUserDto.UserName != user.UserName)
        {
            user.UserName = updateUserDto.UserName;
            isNeedUpdate = true;
        }

        if (updateUserDto.Email is not null && updateUserDto.Email != user.Email)
        {
            user.Email = updateUserDto.Email;
            isNeedUpdate = true;
        }

        if (isNeedUpdate)
        {
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(id, cancellationToken);
        if (user is null)
            return;
        
        _userRepository.Delete(user);
        await _unitOfWork.CommitAsync();
    }
}
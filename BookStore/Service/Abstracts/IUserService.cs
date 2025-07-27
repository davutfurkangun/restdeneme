using BookStore.Dto;
using BookStore.Entity;

namespace BookStore.Service.Abstracts
{
    public interface IUserService
    {
        UserResponseDto signUp(UserSaveRequestDto userSaveRequestDto);
        string login(UserLoginRequestDto dto);
        User getUserById(string token);
    }
}

using BookStore.Context;
using BookStore.Dto;
using BookStore.Entity;
using BookStore.Entity.Enum;
using BookStore.Service.Abstracts;
using BookStore.Util;

namespace BookStore.Service.Concretes
{
    public class UserService : IUserService
    {
        BsContext context;
        JwtManager jwtManager;
        public UserService(BsContext context, JwtManager jwtManager)
        {
            this.context = context;
            this.jwtManager = jwtManager;
        }
        public UserResponseDto signUp(UserSaveRequestDto userSaveRequestDto)
        {
            User? user = context.Users.FirstOrDefault(x => x.Email == userSaveRequestDto.Email);
            if (user != null)
            {
                throw new Exception("User already exists");
            }
            else
            {
                User user1 = new User();
                user1.Name = userSaveRequestDto.Name;
                user1.Email = userSaveRequestDto.Email;
                user1.Password = userSaveRequestDto.Password;
                user1.Role = Role.USER;


                context.Users.Add(user1);
                context.SaveChanges();

                UserResponseDto userResponseDto = new UserResponseDto();
                userResponseDto.Email = user1.Email;
                userResponseDto.Password = user1.Password;
                userResponseDto.Role = user1.Role;
                return userResponseDto;
            }

        }
        public string login(UserLoginRequestDto dto)
        {
            User? user = context.Users.FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                string token = jwtManager.CreateToken(user.Id);
                return token;
            }

        }
        public User getUserById(string token)
        {
            int userId = int.Parse(jwtManager.ValidateToken(token));
            return context.Users.Find(userId);

        }
    }
}

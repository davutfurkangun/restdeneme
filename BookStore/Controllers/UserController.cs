using BookStore.Dto;
using BookStore.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("signup")]
        public UserResponseDto signUp([FromBody]UserSaveRequestDto userSaveRequestDto)
        {
            return userService.signUp(userSaveRequestDto);
        }
        [HttpPost("login")]
        public string login([FromBody]UserLoginRequestDto dto)
        {
            return userService.login(dto);
        }


    }
   

}

using BookStore.Entity.Enum;

namespace BookStore.Dto
{
    public class UserResponseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } 
    }
}

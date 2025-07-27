using BookStore.Entity.Enum;

namespace BookStore.Dto
{
    public class UserSaveRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}

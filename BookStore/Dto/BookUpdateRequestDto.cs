using BookStore.Entity;

namespace BookStore.Dto
{
    public class BookUpdateRequestDto
    {
        
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}

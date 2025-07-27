namespace BookStore.Dto
{
    public class BookResponseDto
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}

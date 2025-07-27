namespace BookStore.Dto
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly OrderDate { get; set; }
        public List<BookDetailDto> Books { get; set; }
    }
}

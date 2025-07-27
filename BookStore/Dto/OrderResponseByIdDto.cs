namespace BookStore.Dto
{
    public class OrderResponseByIdDto
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly OrderDate {  get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public List<BookDetailDto> Books { get; set; }

    }
}

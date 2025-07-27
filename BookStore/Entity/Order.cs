using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity
{
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
        // public List<OrderItem> OrderItems { get; set; }
        public DateOnly OrderDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
       

    }
}

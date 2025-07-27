using BookStore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BookStore.Context
{
    public class BsContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=DESKTOP-4THI52Q;Database=BookStoreDb;TrustServerCertificate=True;Trusted_Connection=True;");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<OrderItem>().
                HasKey(oi => oi.Id);

            
            modelBuilder.Entity<OrderItem>().
                HasOne(oi => oi.Order)
                   .WithMany()
                   .HasForeignKey(oi => oi.OrderId);


            modelBuilder.Entity<OrderItem>().
                HasOne(oi => oi.Book)
                   .WithMany()
                   .HasForeignKey(oi => oi.Isbn);
           

            modelBuilder.Entity<Order>()
               .HasOne(o => o.User)
               .WithMany()
               .HasForeignKey(o => o.UserId);

        }
    }
}

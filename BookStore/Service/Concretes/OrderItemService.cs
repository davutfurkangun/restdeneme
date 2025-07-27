using BookStore.Context;
using BookStore.Dto;
using BookStore.Entity;
using BookStore.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service.Concretes
{
    public class OrderItemService : IOrderItemService
    {
        BsContext context;

        public OrderItemService(BsContext context)
        {
            this.context = context;
        }

        
        public List<OrderItem> add(int orderId, List<Book> books)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var book in books)
            {
                OrderItem item = new OrderItem();
                item.OrderId = orderId;
                item.Isbn = book.Isbn;

                orderItems.Add(item);
            }

            context.OrderItems.AddRange(orderItems);
            context.SaveChanges();

            return orderItems;
        }

       
        public List<BookDetailDto> bookDetailByOrderId(int orderId)
        {
           
            List<OrderItem> orderItems = context.OrderItems
                .Where(x => x.OrderId == orderId)
                .Include(x => x.Book)
                .ToList();

            List<BookDetailDto> books = new List<BookDetailDto>();

            foreach (var item in orderItems)
            {
                BookDetailDto dto = new BookDetailDto();

                dto.Isbn = item.Book.Isbn;
                dto.Title = item.Book.Title;
                dto.Author = item.Book.Author;
                dto.Price = item.Book.Price;
                books.Add(dto);
            }

            return books;
        }

    }
}

using System.Data;
using BookStore.Context;
using BookStore.Dto;
using BookStore.Entity;
using BookStore.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service.Concretes
{
    public class OrderService : IOrderService
    {
        BsContext context;
        IBookService bookService;
        IOrderItemService orderItemService;
        IUserService userService;

        public OrderService(BsContext context, IBookService bookService, IOrderItemService orderItemService, IUserService userService)
        {
            this.context = context;
            this.bookService = bookService;
            this.orderItemService = orderItemService;
            this.userService = userService;
        }

        public OrderResponseDto add(List<string> BookIsbn, string token)
        {
          
            List<Book> books = bookService.getListBookByIsbn(BookIsbn);
            foreach (Book book in books) 
            {
                if (book.Stock == 0) 
                {
                    throw new Exception("You cannot select books that are out of stock.");
                }

            }

            decimal totalPrice = books.Sum(x => x.Price);
            
            if(totalPrice < 25)
            {
                throw new Exception("Total order must be at least $25.");
            }

            Order order = new Order();
            order.UserId = userService.getUserById(token).Id ;
            order.TotalPrice = totalPrice;
            context.Orders.Add(order);
            
            foreach (Book book in books)
            {
                book.Stock = book.Stock - 1;
            }
            context.SaveChanges();

            orderItemService.add(order.Id,books);
            

            OrderResponseDto orderResponseDto = new OrderResponseDto();
            orderResponseDto.OrderId=order.Id;
            orderResponseDto.UserId = order.UserId;

            return orderResponseDto;
        }
        public List<OrderResponseByIdDto> getOrderByUserId(int userId)
        {
           
            List<Order> orders = context.Orders
                 .Where(x => x.UserId == userId)
                 .OrderByDescending(x => x.UpdatedAt)
                 .ToList();
            List<OrderResponseByIdDto> listDto = new List<OrderResponseByIdDto>();

            foreach (var order in orders)
            {
                OrderResponseByIdDto dto = new OrderResponseByIdDto();

                dto.OrderId = order.Id;
                dto.TotalPrice = order.TotalPrice;
                dto.OrderDate = order.OrderDate;
                dto.UpdateAt = order.UpdatedAt;
                dto.Books = orderItemService.bookDetailByOrderId(order.Id);

                listDto.Add(dto);
            }

                return listDto;
        }
        public OrderDetailDto GetOrderDetail(int orderId)
        {
            Order? order = context.Orders.FirstOrDefault(x=>x.Id==orderId);
            if(order == null)
            {
                throw new Exception("Order not found.");
            }
            OrderDetailDto dto= new OrderDetailDto();
            dto.OrderId=orderId;
            dto.OrderDate=order.OrderDate;
            dto.TotalPrice=order.TotalPrice;
            dto.Books = orderItemService.bookDetailByOrderId(orderId);
            return dto;

        }
    }
}

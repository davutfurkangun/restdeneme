using BookStore.Context;
using BookStore.Dto;
using BookStore.Entity;

namespace BookStore.Service.Abstracts
{
    public interface IOrderItemService
    {
        List<OrderItem> add(int orderId, List<Book> books);
        List<BookDetailDto> bookDetailByOrderId(int orderId);

    }
}

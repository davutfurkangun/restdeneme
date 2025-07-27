using BookStore.Dto;

namespace BookStore.Service.Abstracts
{
    public interface IOrderService
    {
        OrderResponseDto add(List<string> BookIsbn, string token);
        List<OrderResponseByIdDto> getOrderByUserId(int userId);
        OrderDetailDto GetOrderDetail(int orderId);
    }
}

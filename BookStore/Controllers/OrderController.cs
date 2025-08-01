using BookStore.Dto;
using BookStore.Service.Abstracts;
using BookStore.Service.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public OrderResponseDto add(List<string> BookIsbn, [FromQuery]string token)
        {
            return orderService.add(BookIsbn,token);
        }

        [HttpGet("{userId}")]
        public List<OrderResponseByIdDto> getOrderByUserId([FromRoute]int userId)
        {
             return orderService.getOrderByUserId(userId);
        }
        [HttpGet("/details/{orderId}")]
        public OrderDetailDto GetOrderDetail([FromRoute]int orderId)
        {
            return orderService.GetOrderDetail(orderId);
        }

    }
}

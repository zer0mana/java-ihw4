using Microsoft.AspNetCore.Mvc;
using OrderHandlerMicroservice.Requests;
using OrderHandlerMicroservice.Responses;

namespace OrderHandlerMicroservice.Controllers;

[ApiController]
[Route("order-handler")]
public class OrderHandlerController : ControllerBase
{

    public OrderHandlerController()
    {
    }

    [HttpPost("create-order")]
    public CreateOrderResponse CreateOrder(CreateOrderRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("get-order-status")]
    public GetOrderStatusResponse GetOrderStatus(GetOrderStatusRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("get-menu")]
    public GetMenuResponse GetMenu(GetMenuRequest request)
    {
        throw new NotImplementedException();
    }
    
    // Обработка заказов - хостед сервис ?
    
    // Проверять наличие блюд
}
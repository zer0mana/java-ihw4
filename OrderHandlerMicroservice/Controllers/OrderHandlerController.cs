using Microsoft.AspNetCore.Mvc;
using OrderHandlerMicroservice.Repositories.Entities;
using OrderHandlerMicroservice.Requests;
using OrderHandlerMicroservice.Responses;
using OrderHandlerMicroservice.Services;

namespace OrderHandlerMicroservice.Controllers;

[ApiController]
[Route("order-handler")]
public class OrderHandlerController : ControllerBase
{
    private readonly OrderHandlerService _orderHandlerService;
    public OrderHandlerController(OrderHandlerService orderHandlerService)
    {
        _orderHandlerService = orderHandlerService;
    }

    [HttpPost("create-order")]
    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
    {
        await _orderHandlerService.AddNewOrders();
        return new CreateOrderResponse(1);
    }
    
    [HttpPost("get-order-status")]
    public GetOrderStatusResponse GetOrderStatus(GetOrderStatusRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("get-menu")]
    public GetMenuResponse GetMenu(GetMenuRequest request)
    {
        var dishes = _orderHandlerService.GetAllDishes();
        var dishesList = dishes
                .Select(x => new MenuItem(x.Id, x.Name, x.Description, x.Price))
                .ToArray();

        var response = new GetMenuResponse(dishesList);
        return response;
    }
    
    [HttpPost("add-dishes")]
    public async Task<AddDishesResponse> AddDishes(AddDishesRequest request)
    {
        var ids = await _orderHandlerService.AddNewDishes(
            request.Dishes.Select(x
                => new DishEntityV1(
                    0,
                    x.Name,
                    x.Description,
                    x.Price,
                    x.Quantity))
                .ToArray());

        var response = new AddDishesResponse(ids);

        return response;
    }
    
    [HttpPost("update-dish")]
    public UpdateDishResponse UpdateDish(UpdateDishRequest request)
    {
        throw new NotImplementedException();
    }

    // Обработка заказов - хостед сервис ?
    
    // Проверять наличие блюд
}
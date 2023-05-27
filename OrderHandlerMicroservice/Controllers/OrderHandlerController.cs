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
        var dishes = request.Dishes.Select(x => x.DishId).ToArray();
        // Сначала проверить блюда по айди, узнать их цену
        var dishesById = await _orderHandlerService.GetDishesById(dishes);

        if (dishesById.Length != dishes.Length)
        {
            // Исключение
            throw new NotImplementedException();
        }
        
        var orderId = await _orderHandlerService.AddNewOrder(new OrderEntityV1(
            0,
            request.UserId,
            "В ожидании",
            request.SpecialRequests,
            DateTimeOffset.Now,
            DateTimeOffset.Now));

        int index = 0;
        var dishesQuantity = request.Dishes.Select(x => x.Quantity).ToArray();

        var orderDishes = dishesById.Select(x => new OrderDishEntityV1(
            0,
            orderId,
            x.Id,
            dishesQuantity[index++],
            x.Price)).ToArray();
        
        // Затем узнать айди, который получил заказ и после этого добавить их в order_dish
        await _orderHandlerService.AddNewOrderDishes(orderDishes);

        return new CreateOrderResponse(orderId);
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
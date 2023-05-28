using Microsoft.AspNetCore.Mvc;
using OrderHandlerMicroservice.Repositories;
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
    private readonly AuthorizationRepository _authorizationRepository;
    public OrderHandlerController(
        OrderHandlerService orderHandlerService,
        AuthorizationRepository authorizationRepository)
    {
        _orderHandlerService = orderHandlerService;
        _authorizationRepository = authorizationRepository;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var dishes = request.Dishes.Select(x => x.DishId).ToArray();
        // Сначала проверить блюда по айди, узнать их цену
        var dishesById = await _orderHandlerService.GetDishesById(dishes);

        if (dishesById.Length != dishes.Length)
        {
            // Исключение
            return StatusCode(StatusCodes.Status400BadRequest, "Dish with this id not exist.");
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

        return StatusCode(StatusCodes.Status200OK, $"Order created with id {orderId}.");
    }
    
    [HttpPost("get-order-status")]
    public IActionResult GetOrderStatus(GetOrderStatusRequest request)
    {
        var order = _orderHandlerService.GetOrderStatus(request.OrderId).Result;

        if (order == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Order with this id not exist.");
        }
        
        return StatusCode(StatusCodes.Status200OK, $"Order status: {order.Status}");
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
    public async Task<IActionResult> AddDishes(AddDishesRequest request)
    {
        var session = await _authorizationRepository.GetSessionByToken(request.Token);

        if (session == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Bad token.");
        }

        var user = await _authorizationRepository.GetUserById(session.UserId);

        if (user.Role != "manager")
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Not enough rights.");
        }
        
        var ids = await _orderHandlerService.AddNewDishes(
            request.Dishes.Select(x
                => new DishEntityV1(
                    0,
                    x.Name,
                    x.Description,
                    x.Price,
                    x.Quantity))
                .ToArray());

        var stringIds = "";
        foreach (var item in ids)
        {
            stringIds += item + " ";
        }
        return StatusCode(StatusCodes.Status200OK, $"Dishes adds with ids {stringIds}.");;
    }
    
    [HttpPost("update-dish")]
    public UpdateDishResponse UpdateDish(UpdateDishRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("run-updater")]
    public void UpdateDish()
    {
        _orderHandlerService.UpdaterRun();
    }

    // Обработка заказов - хостед сервис ?
    
    // Проверять наличие блюд
}
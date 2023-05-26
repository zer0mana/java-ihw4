using OrderHandlerMicroservice.Repositories;
using OrderHandlerMicroservice.Repositories.Entities;
using OrderHandlerMicroservice.Repositories.Migrations;

namespace OrderHandlerMicroservice.Services;

public class OrderHandlerService
{
    private readonly DishRepository _dishRepository;
    private readonly OrderRepository _orderRepository;
    private readonly OrderDishRepository _orderDishRepository;

    public OrderHandlerService(
        DishRepository dishRepository,
        OrderRepository orderRepository,
        OrderDishRepository orderDishRepository)
    {
        _dishRepository = dishRepository;
        _orderRepository = orderRepository;
        _orderDishRepository = orderDishRepository;
    }

    public void Add()
    {
        throw new NotImplementedException();
    }

    public async Task<int[]> AddNewDish()
    {
        var result = await _dishRepository.Add(
            new[] { new DishEntityV1(1, "max", "lol", 100, 1) },
            CancellationToken.None);

        return result;
    }
    
    public async Task<int[]> AddNewOrders()
    {
        var result = await _orderRepository.Add(
            new[] { new OrderEntityV1(1, 1, "lol", "max", DateTime.Now, DateTime.Now) }, 
            CancellationToken.None);

        return result;
    }
    
    public void UpdateDish()
    {
        
    }
}
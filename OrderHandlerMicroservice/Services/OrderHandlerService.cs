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

    public async Task<int[]> AddNewDishes(DishEntityV1[] entityV1)
    {
        var result = await _dishRepository.Add(
            entityV1,
            CancellationToken.None);

        return result;
    }
    
    public async Task<int> AddNewOrder(OrderEntityV1 entity)
    {
        var result = await _orderRepository.Add(
            new[] { entity }, 
            CancellationToken.None);

        return result.First();
    }

    public async Task<DishEntityV1[]> GetDishesById(int[] dishesIds)
    {
        var entities = await _dishRepository.GetDishesById(dishesIds);

        return entities;
    }

    public async Task<int[]> AddNewOrderDishes(OrderDishEntityV1[] entityV1)
    {
        var result = await _orderDishRepository.Add(
            entityV1);

        return result;
    }
    public void UpdateDish()
    {
        
    }

    public DishEntityV1[] GetAllDishes()
    {
        var dishes = _dishRepository.GetAllDishes();

        return dishes.Result;
    }
}
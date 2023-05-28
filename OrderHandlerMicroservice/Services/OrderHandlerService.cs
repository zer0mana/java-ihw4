using OrderHandlerMicroservice.Repositories;
using OrderHandlerMicroservice.Repositories.Entities;
using OrderHandlerMicroservice.Repositories.Migrations;

namespace OrderHandlerMicroservice.Services;

public class OrderHandlerService
{
    private readonly DishRepository _dishRepository;
    private readonly OrderRepository _orderRepository;
    private readonly OrderDishRepository _orderDishRepository;
    
    private bool _updaterActive;

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

    public void UpdaterRun()
    {
        if (_updaterActive)
        {
            throw new NotImplementedException();
        }

        _updaterActive = true;
        Task.Run(Updater);
    }

    private async void Updater()
    {
        // Беру какой-то заказ со статусом "в ожидании"
        while (true)
        {
            var order = (await _orderRepository.GetWaitingOrders(1)).FirstOrDefault();

            if (order == null)
            {
                await Task.Delay(10000);
                continue;
            }
            // Ищу все order_dish с его id
            var orderDishes = await _orderDishRepository.GetByOrderId(order.Id);

            // По их id получаю все quantity из dish
            var dishesIds = orderDishes.Select(x => x.DishId).ToArray();
            var dishes = _dishRepository.GetDishesById(dishesIds).Result;

            var newDishes = new List<DishEntityV1>();

            bool flag = true;
            foreach (var item in orderDishes)
            {
                var dish = dishes.First(x => x.Id == item.DishId);
                
                if (dish.Quantity < item.Quantity)
                {
                    flag = false;
                }
                else
                {
                    dish.Quantity = dish.Quantity - item.Quantity;
                    newDishes.Add(dish);
                }
            }

            foreach (var item in newDishes)
            {
                Console.WriteLine(item.Quantity);
            }
            // Если не хватает блюд, то отменяю заказ
            if (!flag)
            {
                _orderRepository.UpdateOrder(order.Id, "Отменен");
            }
            // Иначе меняю
            else
            {
                _dishRepository.Update(newDishes.ToArray());
                _orderRepository.UpdateOrder(order.Id, "Выполнен");
            }

            await Task.Delay(10000);
        }
    }
}
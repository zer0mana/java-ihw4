using OrderHandlerMicroservice.Repositories.Entities;

namespace Interfaces;

public interface IOrderHandlerService
{
    public Task<int[]> AddNewDishes(DishEntityV1[] entityV1);

    public Task<int> AddNewOrder(OrderEntityV1 entity);

    public Task<OrderEntityV1> GetOrderStatus(int id);

    public Task<DishEntityV1[]> GetDishesById(int[] dishesIds);

    public Task<int[]> AddNewOrderDishes(OrderDishEntityV1[] entityV1);
    public DishEntityV1[] GetAllDishes();

    public void UpdaterRun();
}
using OrderHandlerMicroservice.Repositories.Migrations;

namespace OrderHandlerMicroservice.Services;

public class OrderHandlerService
{
    private readonly OrderDishRepository _orderDishRepository;

    public OrderHandlerService(OrderDishRepository orderDishRepository)
    {
        _orderDishRepository = orderDishRepository;
    }

    public void Add()
    {
        _orderDishRepository.Add();
    }
}
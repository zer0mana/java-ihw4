namespace OrderHandlerMicroservice.Requests;

public record GetOrderStatusRequest(
    long OrderId);
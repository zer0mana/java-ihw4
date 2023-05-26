using OrderHandlerMicroservice.Repositories.Migrations;

namespace OrderHandlerMicroservice.Repositories.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<DishRepository>();
        services.AddScoped<OrderRepository>();
        services.AddScoped<OrderDishRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        Postgres.MapCompositeTypes();
        
        Postgres.AddMigrations(services);

        return services;
    }
}
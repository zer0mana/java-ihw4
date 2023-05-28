using AuthorizationMicroservice.Repositories.Migrations;

namespace AuthorizationMicroservice.Repositories.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<SessionRepository>();

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
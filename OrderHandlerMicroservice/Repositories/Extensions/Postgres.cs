using FluentMigrator.Exceptions;
using FluentMigrator.Runner;
using Npgsql;
using Npgsql.NameTranslation;
using OrderHandlerMicroservice.Repositories.Entities;

namespace OrderHandlerMicroservice.Repositories.Extensions;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static void MapCompositeTypes()
    {
        var mapper = NpgsqlConnection.GlobalTypeMapper;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        mapper.MapComposite<DishEntityV1>("dish_v1", Translator);
        mapper.MapComposite<OrderEntityV1>("order_v1", Translator);
        mapper.MapComposite<OrderDishEntityV1>("order_dish_v1", Translator);
    }

    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                    "User ID=postgres;Password=123456;Host=localhost;Port=15432;Database=order-handler;Pooling=true;")
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}
using FluentMigrator.Exceptions;
using FluentMigrator.Runner;
using Npgsql;
using Npgsql.NameTranslation;

namespace OrderHandlerMicroservice.Repositories.Extensions;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static void MapCompositeTypes()
    {
        var mapper = NpgsqlConnection.GlobalTypeMapper;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        // mapper.MapComposite<>()
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
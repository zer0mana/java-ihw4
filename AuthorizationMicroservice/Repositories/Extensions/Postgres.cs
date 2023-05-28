using FluentMigrator.Exceptions;
using FluentMigrator.Runner;
using Npgsql;
using Npgsql.NameTranslation;
using AuthorizationMicroservice.Repositories.Entities;

namespace AuthorizationMicroservice.Repositories.Extensions;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static void MapCompositeTypes()
    {
        var mapper = NpgsqlConnection.GlobalTypeMapper;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        mapper.MapComposite<UserEntityV1>("user_v1", Translator);
        mapper.MapComposite<SessionEntityV1>("session_v1", Translator);
    }

    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                    "User ID=postgres;Password=123456;Host=localhost;Port=15433;Database=authorization;Pooling=true;")
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}
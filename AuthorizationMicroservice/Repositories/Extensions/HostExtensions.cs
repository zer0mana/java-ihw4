﻿using FluentMigrator.Runner;

namespace AuthorizationMicroservice.Repositories.Extensions;

public static class HostExtensions
{
    public static IHost MigrateUp(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        return app;
    }
}
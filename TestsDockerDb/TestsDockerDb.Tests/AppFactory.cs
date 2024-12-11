using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace TestsDockerDb.Tests;

public class AppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public readonly MsSqlContainer DatabaseContainer = new MsSqlBuilder()
        .WithAutoRemove(true)
        .Build();

    public async Task InitializeAsync()
    {
        await DatabaseContainer.StartAsync();

        Environment.SetEnvironmentVariable("ConnectionStrings:MyDb", DatabaseContainer.GetConnectionString());

        await DatabaseContainer.ExecScriptAsync(File.ReadAllText("InitDatabaseScript.sql"));
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await DatabaseContainer.StopAsync();
    }
}

using Microsoft.AspNetCore.Mvc.Testing;

namespace TestsMockaco.Tests;

public class AppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public readonly MockacoContainer MockacoContainer = new();

    public async Task InitializeAsync()
    {
        await MockacoContainer.Container.StartAsync();

        Environment.SetEnvironmentVariable("Api:Url", MockacoContainer.ContainerUri);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await MockacoContainer.Container.StopAsync();
    }
}

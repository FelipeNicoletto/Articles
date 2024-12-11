using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace TestsMockaco.Tests;

public class MockacoContainer
{
    private readonly IContainer _container;

    public int ContainerPort { get; }
    public string ContainerUri => $"http://localhost:{ContainerPort}";
    public IContainer Container => _container;

    public MockacoContainer()
    {
        ContainerPort = 4226;

        _container = new ContainerBuilder()
            .WithImage("natenho/mockaco")
            .WithPortBinding(ContainerPort, 5000)
            .WithAutoRemove(true)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .AddCustomWaitStrategy(new MockacoContainerHealthCheck(ContainerUri))
            )
            .WithBindMount(ToAbsolute("./Mockaco"), "/app/Mocks", AccessMode.ReadWrite)
            .Build();
    }

    private static string ToAbsolute(string path) => Path.GetFullPath(path);
}

public class MockacoContainerHealthCheck(string endpoint) : IWaitUntil
{
    public async Task<bool> UntilAsync(IContainer container)
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri(endpoint) };
        
        try
        {
            var result = await httpClient.GetStringAsync("/_mockaco/ready");

            return result?.Equals("Healthy") ?? false;
        }
        catch
        {
            return false;
        }
    }
}

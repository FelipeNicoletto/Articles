namespace TestsMockaco.Tests;

public class Tests(AppFactory appFactory) : IClassFixture<AppFactory>
{
    [Fact]
    public async Task Test()
    {
        var client = appFactory.CreateClient();
        var response = await client.GetStringAsync("myapi");

        Assert.Equal("api result", response);
    }
}
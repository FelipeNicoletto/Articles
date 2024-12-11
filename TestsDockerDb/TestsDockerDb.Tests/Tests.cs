using Dapper;
using Microsoft.Data.SqlClient;
using System.Net.Http.Json;

namespace TestsDockerDb.Tests;

public class Tests(AppFactory appFactory) : IClassFixture<AppFactory>
{
    [Fact]
    public async Task PostUsers_ShouldCreateUserInDatabase()
    {
        var client = appFactory.CreateClient();

        var response = await client.PostAsJsonAsync("/users", new { name = "User Name", email = "useremail@email.com" });

        var id = await response.Content.ReadFromJsonAsync<Guid>();

        // Assert
        using var connection = new SqlConnection(appFactory.DatabaseContainer.GetConnectionString());

        var count = await connection.QueryFirstAsync<int>("SELECT COUNT(1) FROM Users");
        Assert.Equal(1, count);

        var user = await connection.QueryFirstAsync<(string Name, string Email)>("SELECT Name, Email FROM Users WHERE Id = @id", new { id });
        Assert.Equal("User Name", user.Name);
        Assert.Equal("useremail@email.com", user.Email);
    }
}
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace TestsDockerDb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IDbConnection dbConnection) : Controller
{
    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] UserModel user)
    {
        var id = Guid.NewGuid();

        await dbConnection.ExecuteAsync("INSERT INTO Users(Id, Name, Email) VALUES(@id, @Name, @Email)", new { id, user.Name, user.Email });

        return Ok(id);
    }

    public class UserModel
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}

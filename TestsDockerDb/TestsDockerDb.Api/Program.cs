using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("MyDb")!));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

public partial class Program
{
    protected Program() { }
}
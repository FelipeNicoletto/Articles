var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient("externalService", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Api:Url")!);
});

var app = builder.Build();

app.MapControllers();

app.Run();

public partial class Program
{
    protected Program() { }
}
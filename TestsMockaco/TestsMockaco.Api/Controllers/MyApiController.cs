using Microsoft.AspNetCore.Mvc;

namespace TestsMockaco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MyApiController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var httpClient = httpClientFactory.CreateClient("externalService");

        var response = await httpClient.GetFromJsonAsync<ApiResult>("results");

        return Ok(response!.Result);
    }

    private class ApiResult
    {
        public string Result { get; set; } = string.Empty;
    }
}

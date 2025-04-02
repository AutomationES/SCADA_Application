using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCADA.Frontend.ViewModels;
using System.Text.Json;

namespace SCADA.Frontend.Controllers;

[Authorize]
public class TagsController : Controller
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _config;

    public TagsController(IHttpClientFactory httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    // GET: Tags?plcId=5
    public async Task<IActionResult> Index(int? plcId)
    {
        var client = _httpClient.CreateClient("API");
        var url = plcId.HasValue ? $"Tags?plcId={plcId}" : "Tags";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var tags = await response.Content.ReadFromJsonAsync<List<TagViewModel>>();

        ViewBag.SignalRUrl = _config["SignalRHubUrl"];
        return View(tags);
    }

    // GET: Tags/Monitor/5
    public async Task<IActionResult> Monitor(int id)
    {
        var client = _httpClient.CreateClient("API");
        var response = await client.GetAsync($"Tags/{id}");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var tag = await response.Content.ReadFromJsonAsync<TagViewModel>();

        ViewBag.SignalRUrl = _config["SignalRHubUrl"];
        return View(tag);
    }

    // GET: Tags/Configure/5
    [Authorize(Roles = "Admin,Engineer")]
    public async Task<IActionResult> Configure(int id)
    {
        var client = _httpClient.CreateClient("API");
        var response = await client.GetAsync($"Tags/{id}");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var tag = await response.Content.ReadFromJsonAsync<TagViewModel>();

        // Get PLCs for dropdown
        var plcResponse = await client.GetAsync("PLCs");
        var plcs = await plcResponse.Content.ReadFromJsonAsync<List<PLCViewModel>>();

        ViewBag.PLCs = plcs;
        return View(tag);
    }
}
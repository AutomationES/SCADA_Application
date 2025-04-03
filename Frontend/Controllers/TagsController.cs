using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCADA.Frontend.ViewModels;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SCADA.Frontend.Controllers;

[Authorize]
public class TagsController : Controller
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _config;
    private readonly ILogger<TagsController> _logger;

    public TagsController(IHttpClientFactory httpClient, IConfiguration config, ILogger<TagsController> logger)
    {
        _httpClient = httpClient;
        _config = config;
        _logger = logger;
    }

    // GET: Tags?plcId=5
    public async Task<IActionResult> Index(int? plcId)
    {
        var client = _httpClient.CreateClient("API");
        
        var url = plcId.HasValue ? $"Tags?plcId={plcId}" : "Tags";

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var tags = await response.Content.ReadFromJsonAsync<List<TagViewModel>>();
            ViewBag.SignalRUrl = _config["SignalRHubUrl"];
            return View(tags);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "API request failed");
            return View("Error");
        }
        //var client = _httpClient.CreateClient("API");
        //var url = plcId.HasValue ? $"Tags?plcId={plcId}" : "Tags";

        //var response = await client.GetAsync(url);

        //if (!response.IsSuccessStatusCode)
        //    return View("Error");

        //var tags = await response.Content.ReadFromJsonAsync<List<TagViewModel>>();

        //ViewBag.SignalRUrl = _config["SignalRHubUrl"];
        //return View(tags);
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
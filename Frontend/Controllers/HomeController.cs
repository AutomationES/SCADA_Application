using Microsoft.AspNetCore.Mvc;
using SCADA.Frontend.ViewModels;
using System.Net;
using System.Net.Http; // For HttpClient
using System.Net.Http.Headers; // For MediaTypeWithQualityHeaderValue
using System.Net.Http.Json; // For ReadFromJsonAsync

namespace SCADA.Frontend.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    // Inject IHttpClientFactory through constructor
    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("API");
            client.Timeout = TimeSpan.FromSeconds(30); // Set timeout

            var response = await client.GetAsync("dashboard/stats");

            response.EnsureSuccessStatusCode(); // Throws if not 200-299

            var model = await response.Content.ReadFromJsonAsync<DashboardViewModel>();
            return View(model);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }
        catch (HttpRequestException ex)
        {
            // Log error (e.g., using ILogger)
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            // Handle timeout
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
    }
}
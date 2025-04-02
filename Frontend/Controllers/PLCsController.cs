using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCADA.Common.Models;
using SCADA.Frontend.ViewModels;

namespace SCADA.Frontend.Controllers;

[Authorize]
public class PLCsController : Controller
{
    private readonly IHttpClientFactory _httpClient;

    public PLCsController(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClient.CreateClient("API");
        var response = await client.GetAsync("PLCs?includeTags=true");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var plcs = await response.Content.ReadFromJsonAsync<List<PLC>>(); // First get as domain model
        var viewModels = plcs.Select(p => new PLCViewModel
        {
            Id = p.Id,
            Name = p.Name,
            IPAddress = p.IPAddress,
            Port = p.Port,
            ProtocolType = p.ProtocolType.ToString(),
            IsActive = p.IsActive,
            Tags = p.Tags?.Select(t => new TagViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Address = t.Address,
                DataType = t.DataType.ToString()
            }).ToList()
        }).ToList();

        return View(viewModels);
    }


}
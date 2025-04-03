using SCADA.Common.Models;
using System.Net.Http.Headers;

namespace SCADA.Frontend.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;

        // Set base address from configuration
        _httpClient.BaseAddress = new Uri(_config["ApiBaseUrl"]);
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Tag>> GetTagsAsync()
    {
        var response = await _httpClient.GetAsync("Tags");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Tag>>();
    }
}
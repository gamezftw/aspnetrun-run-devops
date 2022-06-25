using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shopping.Client.Models;

namespace Shopping.Client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory _httpClientFactory)
    {
        _logger = logger;
        _httpClient = _httpClientFactory.CreateClient("ShoppingAPIClient");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("/product");
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        return View(content);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

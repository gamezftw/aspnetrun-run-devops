using Microsoft.AspNetCore.Mvc;
using Shopping.API.Models;
using Shopping.API.Data;
using MongoDB.Driver;

namespace Shopping.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductContext _context;

    public ProductController(ILogger<ProductController> logger, ProductContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        return await _context.Products.Find(_ => true).ToListAsync();
    }
}
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StreamShop.API.Interfaces;
using StreamShop.API.Models;

namespace StreamShop.API.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository<Product> _productRepository;
    public ProductController(IProductRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet(Name = "GetAllProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = _productRepository.GetAllProducts();
        return Ok(products);
    }
    
    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = _productRepository.GetProductById(id);
        return Ok(product);
    }
    
    [HttpPost(Name = "CreateProduct")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _productRepository.Add(product);
        return Ok(product);
    }

    [HttpPut("{id}", Name = "UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest("Product is null.");
        }
        
        var productToUpdate = _productRepository.GetProductById(id);
        if (productToUpdate == null)
        {
            return NotFound("The Product record couldn't be found.");
        }
        
        _productRepository.Update(productToUpdate, product);
        
        return NoContent();
    }
    
    [HttpDelete("{id}", Name = "DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = _productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound("The Product record couldn't be found.");
        }
        
        _productRepository.Delete(product);
        
        return NoContent();
    }
}
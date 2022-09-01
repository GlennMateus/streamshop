using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StreamShop.API.Interfaces;
using StreamShop.API.Models;

namespace StreamShop.API.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IRepository<Category> _categoryRepository;
    public CategoryController(IProductRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    [HttpGet(Name = "GetCategories")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = _categoryRepository.GetAll();
        return Ok(categories);
    }
    
    [HttpPost(Name = "CreateCategory")]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        _categoryRepository.Add(category);
        return Ok(category);
    }
}
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StreamShop.API.Interfaces;
using StreamShop.API.Models;

namespace StreamShop.API.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("api/[controller]")]
public class ProductImagesController : ControllerBase
{
    private readonly IRepository<ProductImages> _productImagesRepository;
    private readonly IProductImagesServices _productImagesServices;

    public ProductImagesController(IRepository<ProductImages> productImagesRepository,
        IProductImagesServices productImagesServices)
    {
        _productImagesRepository = productImagesRepository;
        _productImagesServices = productImagesServices;
    }


    [HttpGet("{productId}", Name = "GetProductImages")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductImages(int productId)
    {
        var productImages = _productImagesRepository.GetAll().Where(pi => pi.ProductId == productId);
        return Ok(productImages);
    }

    [HttpPost("{productId}", Name = "CreateImage")]
    public async Task<ActionResult<ProductImages>> CreateImage(int productId, 
        [FromForm] List<IFormFile> productImages)
    {
        await _productImagesServices.UploadImages(productId, productImages);
        return Ok();
    }

    [HttpDelete("{id}", Name = "DeleteImage")]
    public async Task<ActionResult<ProductImages>> DeleteImage(int id)
    {
        var product = _productImagesRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        _productImagesRepository.Delete(product);
        return NoContent();
    }
}
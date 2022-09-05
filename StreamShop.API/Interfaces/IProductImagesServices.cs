using StreamShop.API.Models;

namespace StreamShop.API.Interfaces;

public interface IProductImagesServices
{
    Task UploadImages(int productId, List<IFormFile> files);
    Task DeleteImages(List<ProductImages> fileNames);
}
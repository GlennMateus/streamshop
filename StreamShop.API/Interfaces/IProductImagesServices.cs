namespace StreamShop.API.Interfaces;

public interface IProductImagesServices
{
    Task UploadImages(List<IFormFile> files);
}
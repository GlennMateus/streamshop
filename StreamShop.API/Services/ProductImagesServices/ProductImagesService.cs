using StreamShop.API.Interfaces;
using StreamShop.API.Models;


namespace StreamShop.API.Services.ProductImagesServices;

public class ProductImagesService : IProductImagesServices
{
    private readonly string ImagesPath = $"{Environment.CurrentDirectory}/images/products";
    
    public async Task UploadImages(List<IFormFile> files)
    {
        CheckAndDirectory();
        await AddImageToDirectory(files);
    }

    private async Task AddImageToDirectory(List<IFormFile> files)
    {
        foreach (var file in files)
        {
            using (FileStream fs = System.IO.File.Create($"{ImagesPath}/{file.FileName}"))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
            }
        }
    }

    private void CheckAndDirectory()
    {
        if (!Directory.Exists(ImagesPath))
        {
            Directory.CreateDirectory(ImagesPath);
        }
    }
}
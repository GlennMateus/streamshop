using StreamShop.API.Interfaces;

namespace StreamShop.API.Services.ProductImages;

public class ProductImagesService : IProductImagesServices
{
    private readonly string ImagesPath = $"{Environment.CurrentDirectory}/images/products";

    public async Task UploadImages(List<IFormFile> files)
    {
        CheckAndDirectory();
        foreach (var file in files)
        {
            using (FileStream fs = System.IO.File.Create(file.FileName))
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
﻿using StreamShop.API.Interfaces;
using StreamShop.API.Models;


namespace StreamShop.API.Services.ProductImagesServices;

public class ProductImagesService : IProductImagesServices
{
    private readonly string ImagesPath = $"{Environment.CurrentDirectory}/images/products";
    private readonly IRepository<ProductImages> _productImagesRepository;

    public ProductImagesService(IRepository<ProductImages> productImagesRepository)
    {
        _productImagesRepository = productImagesRepository;
    }
    
    private void CheckAndDirectory()
    {
        if (!Directory.Exists(ImagesPath))
        {
            Directory.CreateDirectory(ImagesPath);
        }
    }
    
    public async Task UploadImages(int productId, List<IFormFile> files)
    {
        CheckAndDirectory();

        foreach (var file in files)
        {
            var fileIsHighlighted = files.IndexOf(file) == 0;
            var newFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}_{file.FileName}";
            await AddImageToDirectory(file, newFileName);
            AddImageToDatabase(productId, file, newFileName, fileIsHighlighted);
        }
    }

    public async Task DeleteImages(List<ProductImages> files)
    {
        foreach (var file in files)
        {
            DeleteImageFromDatabase(file);
            DeleteImageFromDirectory(file.Name);
        }
    }

    private async Task AddImageToDirectory(IFormFile file, string newFileName)
    {
        using (FileStream fs = File.Create($"{ImagesPath}/{newFileName}"))
        {
            await file.CopyToAsync(fs);
            fs.Flush();
        }
    }

    private void AddImageToDatabase(int productId, IFormFile file, string newFileName, bool isHighlighted)
    {
        _productImagesRepository.Add(new ProductImages
        {
            ProductId = productId,
            Name = $"{newFileName}",
            IsHighlighted = isHighlighted
        });
    }
    
    private void DeleteImageFromDirectory(string fileName)
    {
        File.Delete($"{ImagesPath}/{fileName}");
    }
    
    private void DeleteImageFromDatabase(ProductImages file)
    {
        _productImagesRepository.Delete(file);
    }
}
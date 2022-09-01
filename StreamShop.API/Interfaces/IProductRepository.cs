using StreamShop.API.Models;

namespace StreamShop.API.Interfaces;

public interface IProductRepository<T> : IRepository<T> where T : class
{
    public List<Product> GetAllProducts();
    public Product GetProductById(int id);
}
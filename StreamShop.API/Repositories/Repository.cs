using Microsoft.EntityFrameworkCore.Internal;
using StreamShop.API.Context;
using StreamShop.API.Interfaces;
using StreamShop.API.Models;

namespace StreamShop.API.Repositories;

public class Repository<T> : IDisposable, IRepository<T>, IProductRepository<T> where T : class
{
    public Repository(SQLiteDbContext context)
    {
        _context = context;
    }

    protected readonly SQLiteDbContext _context;

    public void Dispose()
    {
        _context.Dispose();
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public List<Product> GetAllProducts()
    {
        var products = _context.ProductImages
            .Where(pi=>pi.IsHighlighted)
            .GroupJoin(_context.Product,
                productImage => productImage.ProductId,
                product => product.Id,
                (productImage, product) => new { productImage, product })
            .SelectMany(
                x => x.product.DefaultIfEmpty(),
                (x, product) => new Product
                {
                    Id = x.productImage.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    OriginalPrice = product.OriginalPrice,
                    PromotionPrice = product.PromotionPrice,
                    Images = (product.Images ?? new List<ProductImages> { x.productImage })
                })
            .ToList();

        return products;
    }

    public Product GetProductById(int id)
    {
        var product = _context.Product
            .Join(_context.ProductImages,
                p => p.Id,
                pi => pi.ProductId,
                (p, pi) => new { p, pi })
            .Select(x => new Product
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Description = x.p.Description,
                OriginalPrice = x.p.OriginalPrice,
                PromotionPrice = x.p.PromotionPrice,
                Images = new List<ProductImages>
                {
                    new ProductImages { Id = x.pi.Id, ProductId = x.pi.ProductId, Name = x.pi.Name }
                }
            })
            .FirstOrDefault(p => p.Id == id);
        return product;
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(T entityToDelete)
    {
        _context.Set<T>().Remove(entityToDelete);
    }

    public void Update(T entityToUpdate, T entity)
    {
        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
    }
}
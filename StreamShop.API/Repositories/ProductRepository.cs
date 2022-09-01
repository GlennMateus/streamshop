using StreamShop.API.Context;
using StreamShop.API.Models;

namespace StreamShop.API.Repositories;

public class ProductRepository : Repository<Product>
{
    public ProductRepository(SQLiteDbContext context) : base(context)
    {
    }
}
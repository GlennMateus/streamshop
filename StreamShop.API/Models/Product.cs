namespace StreamShop.API.Models;

public class Product
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal PromotionPrice { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }  
    public List<ProductImages>? Images { get; set; }
}
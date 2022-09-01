namespace StreamShop.API.Models;

public class ProductImages
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public bool IsHighlighted { get; set; }
    public int ProductId { get; set; }
}
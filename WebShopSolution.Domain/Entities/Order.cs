using System.ComponentModel;

namespace WebShop;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public List<Product> Products { get; set; }

    [DefaultValue("Standard")]
    public string OrderType { get; set; } = "Standard";

    public int Discount { get; set; }

}
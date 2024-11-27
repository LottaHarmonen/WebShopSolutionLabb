namespace WebShop;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public List<Product> Products { get; set; }

    public DateTime OrderDate { get; set; }


}
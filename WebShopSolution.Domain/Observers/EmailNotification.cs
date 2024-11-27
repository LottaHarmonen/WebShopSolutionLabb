namespace WebShop.Notifications
{
    public class EmailNotification : INotificationObserver
    {
        public void Update(Product product)
        {
            Console.WriteLine($"Email Notification: New product added - {product.Name}");
        }
    }
}

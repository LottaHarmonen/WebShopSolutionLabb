using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShop.DataAccess.Data;
using WebShop.DataAccess.Repositories.Order;

namespace WebShopTests.RepositoryTests;

public class OrderRepositoryTests
{
    [Fact]
    public async Task AddOrder_StoresOrderInDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestOrderDatabaseAdd")
            .Options;
        var context = new MyDbContext(options);
        var orderRepository = new OrderRepository(context);
        var order = new Order()
        {
            Id = 1,
            UserId = 1
        };

        //Act
        await orderRepository.Add(order);
        await context.SaveChangesAsync();

        //Assert
        var storedOrder = context.Orders.Find(order.Id);
        Assert.NotNull(storedOrder);
        Assert.Equal(order.UserId, order.UserId);
    }

    [Fact]
    public async Task GetOrder_GetsCorrectOrderFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestOrderDatabaseGet")
            .Options;

        var context = new MyDbContext(options);
        var orderRepository = new OrderRepository(context);
        var order = new Order()
        {
            Id = 1,
            UserId = 1
        };

        //Act
        await orderRepository.Add(order);
        await context.SaveChangesAsync();

        //Act
        var orderFromDatabase = await orderRepository.GetById(1);

        //Assert
        Assert.NotNull(orderFromDatabase);
        Assert.Equal(order.Id, orderFromDatabase.Id);
        Assert.Equal(order.UserId, orderFromDatabase.UserId);
    }

    [Fact]
    public async Task GetAllOrder_GetsAllOrdersFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestOrderDatabaseGetAll")
            .Options;

        var context = new MyDbContext(options);
        var orderRepository = new OrderRepository(context);

        var listOfOrders = new List<Order>
        {
            new(){ Id = 1, Products = new List<Product>()},
            new(){ Id = 2, Products = new List<Product>()},
            new(){ Id = 3, Products = new List<Product>()}
        };

        foreach (var order in listOfOrders)
        {
            await orderRepository.Add(order);
        }
        context.SaveChanges();

        //Act
        var allOrdersFromDatabase = await orderRepository.GetAll();

        //Assert
        Assert.NotNull(allOrdersFromDatabase);
        Assert.Equal(allOrdersFromDatabase, listOfOrders);
        Assert.Equal(3, allOrdersFromDatabase.Count());
        Assert.Contains(allOrdersFromDatabase, c => c.Id == 1);
    }

    //Update
    [Fact]
    public async Task UpdateOrder_UpdateCorrectOrderFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestOrderDatabaseUpdate")
            .Options;

        var context = new MyDbContext(options);
        var orderRepository = new OrderRepository(context);

        var listOfOrders = new List<Order>
        {
            new(){ Id = 1, Products = new List<Product>()},
            new(){ Id = 2, Products = new List<Product>()},
            new(){ Id = 3, Products = new List<Product>()}
        };

        foreach (var order in listOfOrders)
        {
            await orderRepository.Add(order);
        }
        await context.SaveChangesAsync();

        //Act
        var orderToUpdate = context.Orders.First(u => u.Id == 2);
        orderToUpdate.UserId = 1;

        await orderRepository.Update(orderToUpdate);
        await context.SaveChangesAsync();

        //Assert
        var updatedOrder = context.Orders.First(u => u.Id == 2);
        Assert.Equal(1, updatedOrder.UserId);
    }


    //Delete
    [Fact]
    public async Task DeleteOrder_DeleteCorrectOrderFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestOrderDatabaseDelete")
            .Options;

        var context = new MyDbContext(options);
        var orderRepository = new OrderRepository(context);

        var listOfOrders = new List<Order>
        {
            new(){ Id = 1, Products = new List<Product>()},
            new(){ Id = 2, Products = new List<Product>()},
            new(){ Id = 3, Products = new List<Product>()}
        };

        foreach (var order in listOfOrders)
        {
            await orderRepository.Add(order);
        }
        await context.SaveChangesAsync();

        //Act
        var orderToDelete = context.Orders.First(u => u.Id == 2);
        await orderRepository.Delete(orderToDelete.Id);
        await context.SaveChangesAsync();

        //Assert
        var remainingOrders = context.Orders.ToList();
        Assert.Equal(2, remainingOrders.Count);
        Assert.DoesNotContain(remainingOrders, u => u.Id == 2);
    }
}
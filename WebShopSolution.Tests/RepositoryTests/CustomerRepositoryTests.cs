using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShop.DataAccess.Data;
using WebShop.DataAccess.Repositories.User;

namespace WebShopTests.RepositoryTests;

public class CustomerRepositoryTests
{
    [Fact]
    public async Task AddCustomer_StoresCustomerInDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCustomerDatabaseAdd")
            .Options;
        var context = new MyDbContext(options);
        var customerRepository = new CustomerRepository(context);
        var customer = new Customer()
        {
            Email = "email",
            Name = "name"
        };

        //Act
        await customerRepository.Add(customer);
        await context.SaveChangesAsync();

        //Assert
        var storedCustomer = await context.Customers.FirstOrDefaultAsync(u => u.Name == customer.Name);
        Assert.NotNull(storedCustomer);
        Assert.Equal(customer.Name, storedCustomer.Name);
    }

    [Fact]
    public async Task GetCustomer_GetsCorrectCustomerFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCustomerDatabaseGet")
            .Options;

        var context = new MyDbContext(options);
        var customerRepository = new CustomerRepository(context);

        var customer = new Customer()
        {
            Email = "email",
            Name = "name",
            Id = 1
        };
        await customerRepository.Add(customer);
        await context.SaveChangesAsync();

        //Act
        var customerFromDatabase = await customerRepository.GetById(1);

        //Assert
        Assert.NotNull(customerFromDatabase);
        Assert.Equal(customer.Id, customerFromDatabase.Id);
        Assert.Equal(customer.Name, customerFromDatabase.Name);
    }

    [Fact]
    public async Task GetAllCustomers_GetsAllCustomersFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCustomerDatabaseGetAll")
            .Options;

        var context = new MyDbContext(options);
        var customerRepository = new CustomerRepository(context);

        var listOfCustomers = new List<Customer>
        {
            new(){ Id = 1, Name = "customer1", Email = "email1"},
            new(){ Id = 2, Name = "customer2", Email = "email2"},
            new(){ Id = 3, Name = "customer3", Email = "email3"},
        };

        foreach (var customer in listOfCustomers)
        {
            await customerRepository.Add(customer);
        }
        await context.SaveChangesAsync();

        //Act
        var allCustomersFromDatabase = await customerRepository.GetAll();

        //Assert
        Assert.NotNull(allCustomersFromDatabase);
        Assert.Equal(allCustomersFromDatabase, listOfCustomers);
        Assert.Equal(3, allCustomersFromDatabase.Count());
        Assert.Contains(allCustomersFromDatabase, c => c.Name == "customer1");
    }

    //Update
    [Fact]
    public async Task UpdateCustomer_UpdateCorrectCustomerFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCustomerDatabaseUpdate")
            .Options;

        var context = new MyDbContext(options);
        var customerRepository = new CustomerRepository(context);

        var listOfCustomers = new List<Customer>
        {
            new(){ Id = 1, Name = "customer1", Email = "email1"},
            new(){ Id = 2, Name = "customer2", Email = "email2"},
            new(){ Id = 3, Name = "customer3", Email = "email3"},
        };

        foreach (var customer in listOfCustomers)
        {
            await customerRepository.Add(customer);
        }
        await context.SaveChangesAsync();

        //Act
        var customerToUpdate = context.Customers.First(u => u.Id == 2);
        customerToUpdate.Name = "UpdatedName";
        customerToUpdate.Email = "updatedemail@mail.com";

        await customerRepository.Update(customerToUpdate);
        await context.SaveChangesAsync();

        //Assert
        var updatedCustomer = context.Customers.First(u => u.Id == 2);
        Assert.Equal("UpdatedName", updatedCustomer.Name);
        Assert.Equal("updatedemail@mail.com", updatedCustomer.Email);
    }


    //Delete
    [Fact]
    public async Task DeleteCustomer_DeleteCorrectCustomerFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCustomerDatabaseDelete")
            .Options;

        var context = new MyDbContext(options);
        var customerRepository = new CustomerRepository(context);

        var listOfCustomers = new List<Customer>
        {
            new(){ Id = 1, Name = "customer1", Email = "email1"},
            new(){ Id = 2, Name = "customer2", Email = "email2"},
            new(){ Id = 3, Name = "customer3", Email = "email3"},
        };
        foreach (var customer in listOfCustomers)
        {
            await customerRepository.Add(customer);
        }
        await context.SaveChangesAsync();

        //Act
        var customerToDelete = context.Customers.First(u => u.Id == 2);
        await customerRepository.Delete(customerToDelete.Id);
        await context.SaveChangesAsync();

        //Assert
        var remainingCustomers = context.Customers.ToList();
        Assert.Equal(2, remainingCustomers.Count);
        Assert.DoesNotContain(remainingCustomers, u => u.Id == 2);
    }
}
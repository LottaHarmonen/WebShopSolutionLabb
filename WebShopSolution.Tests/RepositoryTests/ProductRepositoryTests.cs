using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Data;
using WebShop.DataAccess.Repositories.Product;

namespace WebShopTests.RepositoryTests;

public class ProductRepositoryTests
{
    [Fact]
    public async Task AddNewProduct_StoresProductInDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestProductDatabaseAdd")
            .Options;
        var context = new MyDbContext(options);
        var productRepository = new ProductRepository(context);
        var product = new WebShop.Product()
        {
            Name = "Product"
        };

        //Act
        await productRepository.Add(product);
        await context.SaveChangesAsync();

        //Assert
        var storedProduct = context.Products.FirstOrDefault(u => u.Name == product.Name);
        Assert.NotNull(storedProduct);
        Assert.Equal(product.Name, storedProduct.Name);
    }

    [Fact]
    public async Task GetProduct_GetsCorrecProductFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestProductDatabaseGet")
            .Options;

        var context = new MyDbContext(options);
        var productRepository = new ProductRepository(context);

        var product = new WebShop.Product()
        {
            Name = "Product"

        };
        await productRepository.Add(product);
        await context.SaveChangesAsync();

        //Act
        var productFromDatabase = await productRepository.GetById(1);

        //Assert
        Assert.NotNull(productFromDatabase);
        Assert.Equal(product.Id, productFromDatabase.Id);
        Assert.Equal(product.Name, productFromDatabase.Name);
    }

    [Fact]
    public async Task GetAllProducts_GetsAllProductsFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestProductDatabaseGetAll")
            .Options;

        var context = new MyDbContext(options);
        var productRepository = new ProductRepository(context);

        var listOfProducts = new List<WebShop.Product>
        {
            new(){ Id = 1, Name = "product1"},
            new(){ Id = 2, Name = "product2"},
            new(){ Id = 3, Name = "product3"},
        };

        foreach (var product in listOfProducts)
        {
            productRepository.Add(product);
        }
        await context.SaveChangesAsync();

        //Act
        var allProductsFromDatabase = await productRepository.GetAll();

        //Assert
        Assert.NotNull(allProductsFromDatabase);
        Assert.Equal(allProductsFromDatabase, listOfProducts);
        Assert.Equal(3, allProductsFromDatabase.Count());
        Assert.Contains(allProductsFromDatabase, c => c.Name == "product1");
    }

    //Update
    [Fact]
    public async Task UpdateProduct_UpdateCorrectProductFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestProductDatabaseUpdate")
            .Options;

        var context = new MyDbContext(options);
        var productRepository = new ProductRepository(context);

        var listOfProducts = new List<WebShop.Product>
        {
            new(){ Id = 1, Name = "product1"},
            new(){ Id = 2, Name = "product2"},
            new(){ Id = 3, Name = "product3"},
        };

        foreach (var product in listOfProducts)
        {
            productRepository.Add(product);
        }
        await context.SaveChangesAsync();

        //Act
        var productToUpdate = context.Products.First(u => u.Id == 2);
        productToUpdate.Name = "UpdatedName";

        await productRepository.Update(productToUpdate);
        await context.SaveChangesAsync();

        //Assert
        var updatedProduct = context.Products.First(u => u.Id == 2);
        Assert.Equal("UpdatedName", updatedProduct.Name);
    }


    //Delete
    [Fact]
    public async Task DeleteProduct_DeleteCorrectProductFromDatabase()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestProductDatabaseDelete")
            .Options;

        var context = new MyDbContext(options);
        var productRepository = new ProductRepository(context);

        var listOfProducts = new List<WebShop.Product>
        {
            new(){ Id = 1, Name = "product1"},
            new(){ Id = 2, Name = "product2"},
            new(){ Id = 3, Name = "product3"},
        };

        foreach (var product in listOfProducts)
        {
            productRepository.Add(product);
        }
        await context.SaveChangesAsync();

        //Act
        var productToDelete = context.Products.First(u => u.Id == 2);
        await productRepository.Delete(productToDelete.Id);
        await context.SaveChangesAsync();

        //Assert
        var remainingProducts = context.Products.ToList();
        Assert.Equal(2, remainingProducts.Count);
        Assert.DoesNotContain(remainingProducts, u => u.Id == 2);
    }
}
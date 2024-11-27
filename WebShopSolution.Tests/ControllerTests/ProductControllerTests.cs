using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShop.Services.Product;

public class ProductControllerTests
{
    private readonly IProductService _fakeProductService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _fakeProductService = A.Fake<IProductService>();
        _controller = new ProductController(_fakeProductService);

    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WithAListOfProducts()
    {
        // Arrange
        var mockProducts = A.CollectionOfFake<Product>(3);

        // Set up the fake service to return the mocked products
        A.CallTo(() => _fakeProductService.GetAll()).Returns(mockProducts);

        // Act
        var result = await _controller.GetProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Equal(3, returnedProducts.Count());
    }

    [Fact]
    public async Task GetProducts_MustHaveHappenedOnce()
    {
        // Arrange
        var mockProducts = A.CollectionOfFake<Product>(3);

        // Set up the fake service to return the mocked products
        A.CallTo(() => _fakeProductService.GetAll()).Returns(mockProducts);

        // Act
        await _controller.GetProducts();

        // Assert
        A.CallTo(() => _fakeProductService.GetAll()).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async Task AddProduct_MustHaveHappenedOnce()
    {
        // Arrange
        var mockProduct = A.Fake<Product>();

        A.CallTo(() => _fakeProductService.Add(mockProduct));

        //Act
        await _controller.AddProduct(mockProduct);

        //Assert
        A.CallTo(() => _fakeProductService.Add(mockProduct)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddProduct_ReturnsOkResult_WhenProductIsValid()
    {
        // Arrange
        var mockProduct = A.Fake<Product>();

        // Act
        var result = await _controller.AddProduct(mockProduct);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateProduct_MustHaveHappenedOnce()
    {
        //Arrange
        var mockProduct = A.Fake<Product>();

        //Act
        await _controller.UpdateProduct(mockProduct);

        //Assert
        A.CallTo(() => _fakeProductService.Update(mockProduct)).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async Task UpdateProduct_ReturnsOkResult_WithValidProduct()
    {
        //Arrange
        var mockProduct = A.Fake<Product>();

        //Act
        var result = await _controller.UpdateProduct(mockProduct);

        //Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_CallsDeleteServiceMethod_MustHaveHappenedOnce()
    {
        //Arrange
        var mockProduct = A.Fake<Product>();

        //Act
        await _controller.DeleteProduct(mockProduct);

        //Assert
        A.CallTo(() => _fakeProductService.Delete(mockProduct.Id)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteProduct_ReturnsOkResult_WithValidProduct()
    {
        //Arrange
        var mockProduct = A.Fake<Product>();

        //Act
        var result = await _controller.DeleteProduct(mockProduct);

        //Assert
        Assert.IsType<OkResult>(result);
    }
}

using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShop.Services.Order;

namespace WebShopTests.ControllerTests;

public class OrderControllerTests
{
    private readonly IOrderService _fakeOrderService;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _fakeOrderService = A.Fake<IOrderService>();
        _controller = new OrderController(_fakeOrderService);
    }

    [Fact]
    public async Task GetOrders_ReturnsOkResult_AndValidOrders()
    {
        // Arrange
        var mockOrders = A.CollectionOfFake<Order>(3);

        A.CallTo(() => _fakeOrderService.GetAll()).Returns(mockOrders);

        // Act
        var result = await _controller.GetAllOrders();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Order>>(okResult.Value);
        Assert.Equal(3, returnedProducts.Count());
    }

    [Fact]
    public async Task GetOrders_CallsGetAllServiceMethod_MustHaveHappenedOnce()
    {
        // Arrange

        // Act
        await _controller.GetAllOrders();

        // Assert
        A.CallTo(() => _fakeOrderService.GetAll()).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async Task GetOrderById_ReturnsOkResult_AndValidOrder()
    {
        //Arrange
        var id = 1;
        var fakeOrder = A.Fake<Order>();

        A.CallTo(() => _fakeOrderService.Get(id)).Returns(fakeOrder);

        //Act
        var result = await _controller.GetOrderById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedOrder = Assert.IsAssignableFrom<Order>(okResult.Value);
        Assert.Equal(fakeOrder, returnedOrder);

    }

    [Fact]
    public async Task AddOrder_CallsAddServiceMethod_MustHaveHappenedOnce()
    {
        // Arrange
        var mockOrder = A.Fake<Order>();

        A.CallTo(() => _fakeOrderService.Add(mockOrder));

        //Act
        await _controller.AddNewOrder(mockOrder);

        //Assert
        A.CallTo(() => _fakeOrderService.Add(mockOrder)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateOrder_CallsUpdateServiceMethod_MustHaveHappenedOnce()
    {
        // Arrange
        var mockOrder = A.Fake<Order>();

        A.CallTo(() => _fakeOrderService.Update(mockOrder));

        //Act
        await _controller.UpdateOrder(mockOrder);

        //Assert
        A.CallTo(() => _fakeOrderService.Update(mockOrder)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteOrder_CallsDeleteServiceMethod_MustHaveHappenedOnce()
    {
        // Arrange
        var mockOrder = A.Fake<Order>();

        A.CallTo(() => _fakeOrderService.Delete(mockOrder.Id));

        //Act
        await _controller.DeleteOrder(mockOrder.Id);

        //Assert
        A.CallTo(() => _fakeOrderService.Delete(mockOrder.Id)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteOrder_ReturnsOkResult_WithValidOrderId()
    {
        //Arrange
        var mockOrder = A.Fake<Order>();

        //Act
        var result = await _controller.DeleteOrder(mockOrder.Id);

        //Assert
        Assert.IsType<OkResult>(result);
    }

}
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShop.Services.User;

namespace WebShopTests.ControllerTests;

public class CustomerControllerTests
{
    private readonly ICustomerService _customerService;
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _customerService = A.Fake<ICustomerService>();
        _controller = new CustomerController(_customerService);
    }

    [Fact]
    public async Task GetCustomerById_CallsGetServiceMethod_MustHaveHappenedOnce()
    {
        //Arrange
        var id = 1;
        var mockCustomer = A.Fake<Customer>();

        A.CallTo(() => _customerService.Get(id)).Returns(mockCustomer);

        //Act
        var result = await _controller.GetCustomerById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedOrder = Assert.IsAssignableFrom<Customer>(okResult.Value);
        Assert.Equal(mockCustomer, returnedOrder);
    }

    [Fact]
    public async Task GetAllCustomers_ReturnsOkResult()
    {
        //Arrange
        var mockOrders = A.CollectionOfFake<Customer>(3);

        A.CallTo(() => _customerService.GetAll()).Returns(mockOrders);

        // Act
        var result = await _controller.GetAllCustomers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCustomers = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
        Assert.Equal(3, returnedCustomers.Count());
    }

    [Fact]
    public async Task GetAllCustomers_CallsGetAllServiceMethod_MustHaveHappenedOnce()
    {
        //Arrange

        //Act
        await _controller.GetAllCustomers();

        //Assert
        A.CallTo(() => _customerService.GetAll()).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async Task UpdateCustomer_CallsUpdateServiceMethod_MustHaveHappened()
    {
        //Arrange
        var mockCustomer = A.Fake<Customer>();

        //Act
        await _controller.UpdateCustomer(mockCustomer);

        //Assert
        A.CallTo(() => _customerService.Update(mockCustomer)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsOkResult()
    {
        //Arrange
        var mockCustomer = A.Fake<Customer>();

        //Act
        var result = await _controller.UpdateCustomer(mockCustomer);

        //Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteCustomer_ReturnsOkResult()
    {
        //Arrange
        var mockCustomer = A.Fake<Customer>();

        //Act
        var result = await _controller.DeleteCustomer(mockCustomer.Id);

        //Assert
        Assert.IsType<OkResult>(result);

    }

    [Fact]
    public async Task DeleteCustomer_CallsDeleteServiceMethod_MustHaveHappenedOnce()
    {
        //Arrange
        var mockCustomer = A.Fake<Customer>();

        //Act
        await _controller.DeleteCustomer(mockCustomer.Id);

        //Assert
        A.CallTo(() => _customerService.Delete(mockCustomer.Id)).MustHaveHappenedOnceExactly();
    }

}

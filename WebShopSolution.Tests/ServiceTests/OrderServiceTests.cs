using FakeItEasy;
using WebShop;
using WebShop.DataAccess.Repositories.Order;
using WebShop.Services.Order;
using WebShop.UnitOfWork;

namespace WebShopTests.ServiceTests;

public class OrderServiceTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderServiceTests()
    {
        _orderRepository = A.Fake<IOrderRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();

        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        A.CallTo(() => _unitOfWork.Orders).Returns(_orderRepository);

    }

    [Fact]
    public async Task AddNewOrder_CallsRepositoryAddMethodOnce_WithCorrectOrder()
    {
        // Arrange
        var dummyOrder = A.Dummy<Order>();
        var orderService = new OrderService(_unitOfWork);

        // Act
        await orderService.Add(dummyOrder);

        // Assert
        A.CallTo(() => _orderRepository.Add(dummyOrder)).MustHaveHappenedOnceExactly();

    }

    [Fact]
    public async Task GetAllOrders_CallsRepositoryGetAllMethodOnce()
    {
        //Arrange
        var orderService = new OrderService(_unitOfWork);

        //Act
        await orderService.GetAll();

        //Assert
        A.CallTo(() => _orderRepository.GetAll()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetAllOrders_ReturnsListFromRepository()
    {
        //Arrange
        var orderService = new OrderService(_unitOfWork);
        var fakeOrders = A.Dummy<List<Order>>();

        A.CallTo(() => _orderRepository.GetAll()).Returns(fakeOrders);

        //Act
        var result = await orderService.GetAll();

        //Assert
        Assert.Equal(fakeOrders, result);
    }


    [Fact]
    public async Task UpdateOrder_CallsRepositoryUpdateMethodOnce_WithCorrectOrder()
    {
        //Arrange
        var orderService = new OrderService(_unitOfWork);
        var orderToUpdate = A.Dummy<Order>();

        //Act
        await orderService.Update(orderToUpdate);

        //Assert
        A.CallTo(() => _orderRepository.Update(orderToUpdate)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteOrderById_CallsRepositoryDeleteMethodOnce_WithCorrectOrderId()
    {
        //Arrange
        var orderService = new OrderService(_unitOfWork);
        var orderToDelete = A.Dummy<Order>();

        //Act
        await orderService.Delete(orderToDelete.Id);

        //Assert
        A.CallTo(() => _orderRepository.Delete(orderToDelete.Id)).MustHaveHappenedOnceExactly();
    }
}
using FakeItEasy;
using WebShop;
using WebShop.DataAccess.Repositories.Order;
using WebShop.Services.Order;
using WebShop.Services.User;
using WebShop.UnitOfWork;
using WebShopSolution.Application.Factories;

namespace WebShopTests.ServiceTests;

public class OrderServiceTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly OrderFactoryManager _orderFactoryManager;

    public OrderServiceTests()
    {
        _orderRepository = A.Fake<IOrderRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _orderFactoryManager = A.Fake<OrderFactoryManager>();


        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        A.CallTo(() => _unitOfWork.Orders).Returns(_orderRepository);

    }

    [Fact]
    public async Task AddNewOrder_CallsRepositoryAddMethodOnce_WithCorrectOrder()
    {
        var mockFactoryManager = A.Fake<OrderFactoryManager>();
        var orderService = new OrderService(_unitOfWork, mockFactoryManager);
        // Arrange
        var newOrder = new Order()
        {
            Products = new List<Product>()
        };

        // Act
        await orderService.Add(newOrder);

        // Assert
        A.CallTo(() => _orderRepository.Add(newOrder)).MustHaveHappened();

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
        var orderService = new OrderService(_unitOfWork);
        //Arrange
        var orderToUpdate = new Order
        {
            Id = 1,
            UserId = 1,
            Products = new List<Product>(),
            OrderType = "Standard"
        };

        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);
        //Act
        await orderService.Update(orderToUpdate);

        //Assert
        A.CallTo(() => _orderRepository.Update(orderToUpdate)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteOrderById_CallsRepositoryDeleteMethodOnce_WithCorrectOrderId()
    {
        var orderService = new OrderService(_unitOfWork);
        //Arrange
        var orderToDelete = A.Dummy<Order>();

        //Act
        await orderService.Delete(orderToDelete.Id);

        //Assert
        A.CallTo(() => _orderRepository.Delete(orderToDelete.Id)).MustHaveHappenedOnceExactly();
    }
}
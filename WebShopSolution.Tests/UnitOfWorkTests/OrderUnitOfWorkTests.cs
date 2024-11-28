using FakeItEasy;
using WebShop.DataAccess.Repositories.Order;
using WebShop.Services.Order;
using WebShop.UnitOfWork;
using WebShop;
using WebShopSolution.Application.Factories;

namespace WebShopTests.UnitOfWorkTests;

public class OrderUnitOfWorkTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly OrderFactoryManager _orderFactoryManager;
    private readonly OrderService _orderService;

    public OrderUnitOfWorkTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _orderRepository = A.Fake<IOrderRepository>();
        _orderFactoryManager = A.Fake<OrderFactoryManager>();
        _orderService = new OrderService(_unitOfWork);

    }


    [Fact]
    public async Task AddOrder_CallsRepositoryAdd_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        var orderService = new OrderService(_unitOfWork, _orderFactoryManager);
        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        var order = new Order()
        {
            Products = new List<Product>()
        };

        //Act
        await orderService.Add(order);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateOrder_CallsRepositoryUpdate_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        var product = A.Fake<Order>();

        //Act
        await _orderService.Update(product);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteOrder_CallsRepositoryDelete_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        var product = A.Fake<Order>();

        //Act
        await _orderService.Delete(product.Id);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    //[Fact]
    //public async Task DeleteOrder_CallsRepositoryDelete_AndUnitOfWorkDispose_WhenAnErrorOccurs()
    //{
    //    // Arrange
    //    var unitOfWorkFake = A.Fake<IUnitOfWork>();
    //    var orderRepositoryFake = A.Fake<IOrderRepository>();

    //    // Assuming OrderService is dependent on both IUnitOfWork and IOrderRepository
    //    var orderService = new OrderService(_unitOfWork);
    //    var order = A.Fake<Order>();

    //    // Set up a fake that throws an exception when Delete is called
    //    A.CallTo(() => orderRepositoryFake.Delete(order.Id)).Throws(new Exception("An error occurred"));

    //    // Act
    //    await orderService.Delete(order.Id);

    //    // Assert
    //    // Ensure that Complete is not called, since the transaction is rolled back
    //    A.CallTo(() => unitOfWorkFake.Complete()).MustNotHaveHappened();

    //    // Ensure that Dispose is called on the unitOfWork to roll back the transaction
    //    A.CallTo(() => unitOfWorkFake.Dispose()).MustHaveHappenedOnceExactly();
    //}
}
using FakeItEasy;
using WebShop.DataAccess.Repositories.Order;
using WebShop.Services.Order;
using WebShop.UnitOfWork;
using WebShop;

namespace WebShopTests.UnitOfWorkTests;

public class OrderUnitOfWorkTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderUnitOfWorkTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _orderRepository = A.Fake<IOrderRepository>();
    }


    [Fact]
    public async Task AddOrder_CallsRepositoryAdd_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        var orderService = new OrderService(_unitOfWork);
        var product = A.Fake<Order>();

        //Act
        await orderService.Add(product);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateOrder_CallsRepositoryUpdate_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        var orderService = new OrderService(_unitOfWork);
        var product = A.Fake<Order>();

        //Act
        await orderService.Update(product);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteOrder_CallsRepositoryDelete_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Order>()).Returns(_orderRepository);

        var orderService = new OrderService(_unitOfWork);
        var product = A.Fake<Order>();

        //Act
        await orderService.Delete(product.Id);

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
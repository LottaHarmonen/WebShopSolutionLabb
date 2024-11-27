using FakeItEasy;
using WebShop;
using WebShop.DataAccess.Data;
using WebShop.DataAccess.Repositories.Order;
using WebShop.DataAccess.Repositories.Product;
using WebShop.DataAccess.Repositories.User;
using WebShop.Notifications;
using WebShop.UnitOfWork;

namespace WebShopTests.UnitOfWorkTests
{
    public class UnitOfWorkTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _orderRepository = A.Fake<IOrderRepository>();
            _productRepository = A.Fake<IProductRepository>();
            _customerRepository = A.Fake<ICustomerRepository>();
        }



        [Fact]
        public async Task NotifyProductAdded_CallsObserverUpdate()
        {
            //Arrange
            var product = A.Fake<Product>();
            var fakeObserver = A.Fake<INotificationObserver>();

            var productSubject = new ProductSubject();
            productSubject.Attach(fakeObserver);

            var unitOfWork = new UnitOfWork(
                A.Fake<ICustomerRepository>(),
                A.Fake<IProductRepository>(),
                A.Fake<IOrderRepository>(),
                dbContext: null,
                productSubject);

            //Act
            await unitOfWork.NotifyProductAdded(product);

            //Assert
            A.CallTo(() => fakeObserver.Update(product)).MustHaveHappenedOnceExactly();

        }





    }
}

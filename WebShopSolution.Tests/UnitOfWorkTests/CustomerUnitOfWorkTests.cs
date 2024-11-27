using FakeItEasy;
using WebShop.Services.User;
using WebShop;
using WebShop.DataAccess.Repositories.User;
using WebShop.UnitOfWork;

namespace WebShopTests.UnitOfWorkTests;

public class CustomerUnitOfWorkTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerUnitOfWorkTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _customerRepository = A.Fake<ICustomerRepository>();
    }

    [Fact]
    public async Task AddCustomer_CallsRepositoryAdd_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Customer>()).Returns(_customerRepository);

        var customerService = new CustomerService(_unitOfWork);
        var customer = A.Fake<Customer>();

        //Act
        await customerService.Add(customer);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }
    [Fact]
    public async Task UpdateCustomer_CallsRepositoryUpdate_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Customer>()).Returns(_customerRepository);

        var customerService = new CustomerService(_unitOfWork);
        var customer = A.Fake<Customer>();

        //Act
        await customerService.Update(customer);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteCustomer_CallsRepositoryDelete_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Customer>()).Returns(_customerRepository);

        var customerService = new CustomerService(_unitOfWork);
        var customer = A.Fake<Customer>();

        //Act
        await customerService.Delete(customer.Id);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

}
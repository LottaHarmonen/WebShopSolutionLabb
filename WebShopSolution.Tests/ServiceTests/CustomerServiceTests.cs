using FakeItEasy;
using WebShop;
using WebShop.DataAccess.Repositories.User;
using WebShop.Services.User;
using WebShop.UnitOfWork;

namespace WebShopTests.ServiceTests;

public class CustomerServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public CustomerServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _customerRepository = A.Fake<ICustomerRepository>();

        A.CallTo(() => _unitOfWork.Repository<Customer>()).Returns(_customerRepository);
    }

    [Fact]
    public async Task AddCustomer_CallsRepositoryAddMethod()
    {
        // Arrange
        var dummyCustomer = A.Fake<Customer>();
        var customerService = new CustomerService(_unitOfWork);

        // Act
        await customerService.Add(dummyCustomer);

        // Assert
        A.CallTo(() => _customerRepository.Add(dummyCustomer)).MustHaveHappenedOnceExactly();
    }

    //GetAll
    [Fact]
    public async Task GetAllCustomers_ReturnsCorrectListOfCustomers()
    {
        // Arrange
        var expectedListOfCustomers = A.CollectionOfFake<Customer>(3);
        A.CallTo(() => _customerRepository.GetAll()).Returns(expectedListOfCustomers);
        var customerService = new CustomerService(_unitOfWork);

        // Act
        var result = await customerService.GetAll();

        // Assert
        Assert.Equal(expectedListOfCustomers, result);
    }



    //Update
    [Fact]
    public async Task UpdateCustomer_CallsUpdateCustomerOnce_WithCorrectCustomer()
    {
        // Arrange
        var customerService = new CustomerService(_unitOfWork);
        var customerToUpdate = A.Dummy<Customer>();

        // Act
        await customerService.Update(customerToUpdate);

        // Assert
        A.CallTo(() => _customerRepository.Update(customerToUpdate)).MustHaveHappenedOnceExactly();
    }

    //Delete
    [Fact]
    public async Task DeleteCustomerById_CallsDeleteOnce_WithCorrectId()
    {
        // Arrange
        var customerService = new CustomerService(_unitOfWork);
        int id = 1;

        // Act
        await customerService.Delete(id);

        // Assert
        A.CallTo(() => _customerRepository.Delete(id)).MustHaveHappenedOnceExactly();
    }


}
using FakeItEasy;
using WebShop.DataAccess.Repositories.Product;
using WebShop.Services.Product;
using WebShop.UnitOfWork;

namespace WebShopTests.ServiceTests;

public class ProductServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public ProductServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _productRepository = A.Fake<IProductRepository>();

        A.CallTo(() => _unitOfWork.Repository<WebShop.Product>()).Returns(_productRepository);
    }

    [Fact]
    public async Task AddProduct_CallsRepositoryAddMethod()
    {
        //Arrange
        var dummyProduct = A.Dummy<WebShop.Product>();
        var productService = new ProductService(_unitOfWork);

        //Act
        await productService.Add(dummyProduct);

        //Assert
        A.CallTo(() => _productRepository.Add(dummyProduct)).MustHaveHappenedOnceExactly();
    }


    [Fact]
    public async Task GetAllProducts_ReturnsCorrectListOfProducts()
    {
        //Arrange
        var productService = new ProductService(_unitOfWork);
        var expectedListOfProducts = A.Dummy<List<WebShop.Product>>();

        A.CallTo(() => _productRepository.GetAll()).Returns(expectedListOfProducts);

        //Act
        var result = await productService.GetAll();

        //Assert
        Assert.Equal(expectedListOfProducts, result);
    }


    [Fact]
    public async Task UpdateUser_CallsUpdateOnce_WithCorrectUser()
    {
        //Arrange
        var productService = new ProductService(_unitOfWork);
        var productToUpdate = A.Fake<WebShop.Product>();

        //Act
        await productService.Update(productToUpdate);

        //Assert
        A.CallTo(() => _productRepository.Update(productToUpdate)).MustHaveHappenedOnceExactly();
    }


    [Fact]
    public async Task DeleteProductById_CallsDeleteOnce_WithCorrectId()
    {
        //Arrange
        var productService = new ProductService(_unitOfWork);
        int id = 1;

        //Act
        await productService.Delete(id);

        //Assert
        A.CallTo(() => _productRepository.Delete(id)).MustHaveHappenedOnceExactly();
    }

}
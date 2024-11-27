using FakeItEasy;
using WebShop.DataAccess.Repositories.Product;
using WebShop.Services.Product;
using WebShop.UnitOfWork;
using WebShop;

namespace WebShopTests.UnitOfWorkTests;

public class ProductUnitOfWorkTests
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductUnitOfWorkTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _productRepository = A.Fake<IProductRepository>();
    }


    [Fact]
    public async Task AddProduct_CallsRepositoryAdd_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Product>()).Returns(_productRepository);

        var productService = new ProductService(_unitOfWork);
        var product = A.Fake<Product>();

        //Act
        await productService.Add(product);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateProduct_CallsRepositoryUpdate_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Product>()).Returns(_productRepository);

        var productService = new ProductService(_unitOfWork);
        var product = A.Fake<Product>();

        //Act
        await productService.Update(product);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }
    [Fact]
    public async Task DeleteProduct_CallsRepositoryDelete_AndUnitOfWorkCompleteMethods()
    {
        //Arrange
        A.CallTo(() => _unitOfWork.Repository<Product>()).Returns(_productRepository);

        var productService = new ProductService(_unitOfWork);
        var product = A.Fake<Product>();

        //Act
        await productService.Delete(product.Id);

        //Assert
        A.CallTo(() => _unitOfWork.Complete()).MustHaveHappenedOnceExactly();
    }

}
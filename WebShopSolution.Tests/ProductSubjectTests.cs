using FakeItEasy;
using WebShop;
using WebShop.Notifications;

namespace WebShopSolution.Tests;

public class ProductSubjectTests
{
    [Fact]
    public void Attach_Observer_ObserverGetsNotified()
    {
        //Arrange
        var productSubject = new ProductSubject();
        var observerFake = A.Fake<INotificationObserver>();

        productSubject.Attach(observerFake);
        var product = new Product { Id = 1, Name = "Test Product" };

        //Act
        productSubject.Notify(product);

        //Assert
        A.CallTo(() => observerFake.Update(A<Product>.That.IsSameAs(product))).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Detach_Observer_ObserverDoesNotGetNotified()
    {
        //Arrange
        var productSubject = new ProductSubject();
        var observerFake = A.Fake<INotificationObserver>();

        productSubject.Attach(observerFake);

        var product = new Product { Id = 1, Name = "Test Product" };
        productSubject.Detach(observerFake);

        //Act
        productSubject.Notify(product);

        //Assert
        A.CallTo(() => observerFake.Update(A<Product>.That.IsSameAs(product))).MustNotHaveHappened();
    }
}
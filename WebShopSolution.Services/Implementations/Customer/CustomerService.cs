
using WebShop.UnitOfWork;

namespace WebShop.Services.User;

public class CustomerService(IUnitOfWork unitOfWork) : GenericService<WebShop.Customer>(unitOfWork), ICustomerService;
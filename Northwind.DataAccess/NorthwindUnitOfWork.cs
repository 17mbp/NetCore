using Northwind.Repositories;
using Northwind.UnitOfWork; 
namespace Northwind.DataAccess
{
    public class NorthwindUnitOfWork : IUnityOfWork
    {      
        public NorthwindUnitOfWork(string cnnstr)
        {
            Customer = new CustomerRepository(cnnstr);
            User = new UserRepository(cnnstr);
            Supplier = new SupplierRepository(cnnstr);
            Order = new OrderRepository(cnnstr);
        }

        public ICustomer Customer { get; private set ; }
        public IUserRepository User { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IOrderRepository Order { get; private set; }
    }
}
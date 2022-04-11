using Northwind.Repositories;

namespace Northwind.UnitOfWork
{
    public interface IUnityOfWork
    {
        ICustomer Customer { get;  }
        IUserRepository User { get; }
        ISupplierRepository Supplier { get; }
        IOrderRepository Order { get; }
    }
}
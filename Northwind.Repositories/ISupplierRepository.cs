using System.Collections.Generic;
using Northwind.Model;
namespace Northwind.Repositories
{
    public interface ISupplierRepository:IRepository<Supplier>
    {
        IEnumerable<Supplier> SupplierPagedList(int page, int rows, string term);
    }
}
using Northwind.BusinessLogic.Interfaces;
using Northwind.Model;
using Northwind.UnitOfWork;
using System.Collections.Generic;
namespace Northwind.BusinessLogic.Implementations
{
    public class SupplierLogic : ISupplierLogic
    {
        private readonly IUnityOfWork _unityOfWork;
        public SupplierLogic(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public bool Delete(Supplier supplier)
        {
           return _unityOfWork.Supplier.Delete(supplier);
        }
        public Supplier GetById(int id)
        {
            return _unityOfWork.Supplier.GetById(id);
        }
        public int Insert(Supplier supplier)
        {
            return _unityOfWork.Supplier.Insert(supplier);
        }
        public IEnumerable<Supplier> SupplierPagedList(int page, int row, string termsearch) 
            => _unityOfWork.Supplier.SupplierPagedList(page, row, termsearch);
        public bool Update(Supplier supplier)
        {
           return _unityOfWork.Supplier.Update(supplier);
        }
    }
}
using Northwind.BusinessLogic.Interfaces;
using Northwind.Model;
using Northwind.UnitOfWork;
using System.Collections.Generic;
namespace Northwind.BusinessLogic.Implementations
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly IUnityOfWork _unitOfWork;
        public CustomerLogic(IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<CustomerList> CustomerPagedList(int page, int row)
        {
            return _unitOfWork.Customer.CustomersPagedList(page, row);
        }
        public bool Delete(Customer customer)
        {
            return _unitOfWork.Customer.Delete(customer);
        }
        public Customer GetById(int id)
        {
           return _unitOfWork.Customer.GetById(id);
        }
        public IEnumerable<Customer> GetList()
        {
            return _unitOfWork.Customer.GetList();
        }
        public int Insert(Customer customer)
        {
            return _unitOfWork.Customer.Insert(customer);
        }
        public bool Update(Customer customer)
        {
            return _unitOfWork.Customer.Update(customer);
        }
    }
}
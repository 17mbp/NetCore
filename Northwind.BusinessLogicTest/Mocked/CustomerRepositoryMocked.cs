using AutoFixture;
using Northwind.Model;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Northwind.Repositories;
using Northwind.UnitOfWork;
namespace Northwind.BusinessLogicTest.Mocked
{
    public class CustomerRepositoryMocked
    {
        private readonly List<Customer> _customer;
        public CustomerRepositoryMocked()
        {
            _customer = Customers();
        }
        public IUnityOfWork GetInstance()
        {
            var mocked = new Mock<IUnityOfWork>();
            mocked.Setup(u => u.Customer).Returns(GetCustomerRepositoryMocked());
            return mocked.Object;
        }
        public ICustomerRepository GetCustomerRepositoryMocked()
        {
            var customerMocked = new Mock<ICustomerRepository>();
            customerMocked.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns((int id) => _customer.FirstOrDefault(cus => cus.Id == id));

            customerMocked.Setup(c => c.Insert(It.IsAny<Customer>()))
                .Callback<Customer>((c) => _customer.Add(c) )
                .Returns<Customer>(c => c.Id);

            customerMocked.Setup(c => c.Update(It.IsAny<Customer>()))
               .Callback<Customer>((c) => 
               {
                   _customer.RemoveAll(cs => cs.Id == c.Id);
                   _customer.Add(c);
               })
               .Returns(true);

            return customerMocked.Object;
        }
        private List<Customer> Customers()
        {
            var fixture = new Fixture();
            var customer = fixture.CreateMany<Customer>(30).ToList();
            for (int i = 0; i < 30; i++)
            {
                customer[i].Id = i + 1;
            }
            return customer;
        }
    }
}
using AutoFixture;
using Moq;
using Northwind.Model;
using Northwind.Repositories;
using Northwind.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
namespace Northwind.BusinessLogicTest.Mocked
{
    public class OrderRepositoryMocked
    {
        private readonly List<Order> _orders;
        public OrderRepositoryMocked()
        {
            _orders = GetOrders();
        }
        public IUnityOfWork GetInstance()
        {
            var mocked = new Mock<IUnityOfWork>();
            mocked.Setup(u => u.Order).Returns(GetOrderRepositoryMocked());
            return mocked.Object;
        }
        public IOrderRepository GetOrderRepositoryMocked()
        {
            var orderMocked = new Mock<IOrderRepository>();
            orderMocked.Setup(c => c.GetList( ))
                .Returns(  _orders);
            return orderMocked.Object;
        }       
        public List<Order> GetOrders()
        {
            var fixture = new Fixture();
            var ordrs = fixture.CreateMany<Order>(30).ToList();
            for (int i = 0; i < 30; i++)
            {
                ordrs[i].Id = i + 1;
            }
            return ordrs;
        }
    }
}
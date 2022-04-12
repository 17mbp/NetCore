using Northwind.BusinessLogic.Interfaces;
using Northwind.Model;
using Northwind.UnitOfWork;
using System.Collections.Generic;
namespace Northwind.BusinessLogic.Implementations
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IUnityOfWork _unityOfWork;
        public OrderLogic(IUnityOfWork untiy)
        {
            _unityOfWork = untiy;
        }
        public bool Delete(Order order)
        {
           return _unityOfWork.Order.Delete(order);
        }
        public Order GetById(int Id)
        {
            return _unityOfWork.Order.GetById(Id);
        }
        public OrderList GetOrderById(int orderId)
        {
            return _unityOfWork.Order.GetOrderById(orderId);
        }
        public IEnumerable<OrderList> GetPaginatedOrders(int page, int rows)
        {
            return _unityOfWork.Order.getPaginatedOrder(page, rows);
        }
    }
}
using Northwind.Model;
using System.Collections.Generic;
namespace Northwind.BusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        IEnumerable<OrderList> GetPaginatedOrders(int page, int rows);
        OrderList GetOrderById(int orderId);
        bool Delete(Order order);
        Order GetById(int Id);
        string GetOrderNumber(int ord);
    }
}
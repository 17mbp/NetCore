using Dapper;
using Northwind.Model;
using Northwind.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace Northwind.DataAccess
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(string cnnstring) : base(cnnstring) { }

        public IEnumerable<OrderList> getPaginatedOrder(int page, int rows)
        {
            var parmeters = new DynamicParameters();
            parmeters.Add("@page", page);
            parmeters.Add("@rows", rows);
            using (var conn = new SqlConnection(_cnnstring))
            {
                var reader = conn.QueryMultiple("dbo.get_paginated_orders", parmeters,
                    commandType: CommandType.StoredProcedure);
                var orderlist = reader.Read<OrderList>().ToList();
                var orderItemList = reader.Read<OrderItemList>().ToList();
                foreach (var item in orderlist)
                {
                    item.SetDetails(orderItemList);
                }
                return orderlist;
            }
        }

        public OrderList GetOrderById(int orderId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@orderId", orderId);
            using (var connection = new SqlConnection(_cnnstring))
            {
                var reader = connection.QueryMultiple("dbo.get_orders_by_id", parameters,
                                         commandType: CommandType.StoredProcedure);

                var orderList = reader.Read<OrderList>().ToList();
                var orderItemList = reader.Read<OrderItemList>().ToList();

                foreach (var item in orderList)
                {
                    item.SetDetails(orderItemList);
                }
                return orderList.FirstOrDefault();
            }
        }
    }
}
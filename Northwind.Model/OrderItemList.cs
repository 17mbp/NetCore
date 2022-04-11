using System.Collections.Generic;
using System.Linq;
namespace Northwind.Model
{
    public class OrderItemList
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
     //   public List<OrderItemList> OrderDetails { get; set; }
    //    public void SetDetails(List<OrderItemList> details)
     //   {
    //        OrderDetails = details.Where(x => x.OrderId.Equals(OrderId)).ToList();
      //  }
    }
}
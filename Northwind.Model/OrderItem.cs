namespace Northwind.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
    }
}
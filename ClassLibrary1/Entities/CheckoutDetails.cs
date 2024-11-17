using Domain.Enum;

namespace Domain.Entities
{
    public class CheckoutDetails
    {
        public int AddressID { get; set; }
        public Cart Cart { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public int UserId { get; set; }
    }

    public class OrderRequest
    {
        public string OrderID { get; set; }
        public int UserId { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string VarientID { get; set; }
        public int Quantity { get; set; }
        public int AddressID { get; set; }
        public PaymentMode PaymentModes { get; set; }
        public OrderStatus  OrderStatus { get; set; }
        public string StatusRemark { get; set; }
    }
}

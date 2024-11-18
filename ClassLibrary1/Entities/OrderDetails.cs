using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public int VarientID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ShippingAddress { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public PaymentMode PaymentModes { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string StatusRemark { get; set; }
        public string ProductName { get; set; }
        public string ImageURI { get; set; }
        public string Description { get; set; }
    }

}

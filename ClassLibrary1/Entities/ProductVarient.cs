using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductVarient
    {
        public int ProductVarientId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int VarientId { get; set; }
        public int CategoryName { get; set; }
        public string VarientType { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal SellingPrice{ get; set; }


    }
}

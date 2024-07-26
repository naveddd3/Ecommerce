using Microsoft.AspNetCore.Http;
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
        public int ProductId { get; set; }
        public int VarientTypeId { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

    }

    public class ProductVarientVM : ProductVarient
    {
        public IEnumerable<Varient> varients{ get; set; }
    }

    public class ProductVarientReq : ProductVarient
    {
        public IEnumerable<IFormFile>? productvarientImages { get; set; }
    }

    public class ProductVarientRes : ProductVarient
    {
        public int VarientId { get; set; }
        public string  ProductName { get; set; }
        public string  CategoryName { get; set; }
        public int CategoryId { get; set; }
        public decimal SellingPrice { get; set; }
        public string VarientType { get; set; } 
    }

    public class ProductVarientImages
    {
        public int ProductVarientImageId { get; set; }
        public int ProductVarientId { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}

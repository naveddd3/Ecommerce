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
        public int? VarientId { get; set; }
        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
        public string? ProductName { get; set; }
        public int UnitId { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Quantity { get; set; }  

    }

    public class ProductVarientVM : ProductVarient
    {
        public IEnumerable<MasterUnit> masterUnits{ get; set; }
        
    }

    public class ProductVarientReq : ProductVarient
    {
        public IEnumerable<IFormFile>? productvarientImages { get; set; }=null;
    }

    public class ProductVarientRes : ProductVarient
    {
        
        public string  CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string UnitType { get; set; }
        public string SubCategoryName { get; set; }
        public string? SellingPrice { get; set; }
        public IEnumerable<MasterUnit> masterUnits { get; set; }
    }

    public class ProductVarientImages
    {
        public int ProductVarientImageId { get; set; }
        public int ProductVarientId { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ProductVarientAPIRES
    {
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<ProductVarientRes>? ProductVarients { get; set; }
    }

}

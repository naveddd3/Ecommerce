
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class Product 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal SellingPrice { get; set; }
    }
    public class ProductReq : Product
    {
        public IEnumerable<IFormFile>? Images { get; set; }
    }

    public class ProductVM : Product
    {
        public IEnumerable<MasterCategory> MasterCategories { get; set; }
    }
}

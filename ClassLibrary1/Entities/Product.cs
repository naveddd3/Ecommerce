
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class Product 
    {
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal SellingPrice { get; set; }
        public int? ProductImageId { get; set; }
        public int? VarientCount { get; set; }
    }
    public class ProductReq : Product
    {
        public IEnumerable<IFormFile>? Images { get; set; }
    }

    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageURI { get; set; }
    }

    public class ApiResponseProduct
    {
        public List<Product> Table1 { get; set; }
        public List<ProductImage> Table2 { get; set; }
    }
    public class ProductVM 
    {
        public IEnumerable<SubCategory> SubCategories{ get; set; }

        public IEnumerable<ProductImage>? Images { get; set; }

        public Product product { get; set; }
    }
}

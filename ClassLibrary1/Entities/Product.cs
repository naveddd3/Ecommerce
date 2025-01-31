﻿
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
        public int? UnitId { get; set; }
        public string? UnitType { get; set; }
        public int? Quantity { get; set; }
        public int VendorId { get; set; }
        public bool IsVarient { get; set; }
    }
    public class ProductReq : Product
    {
        public IFormFile? ProductImage { get; set; }
    }

    public class CartProductReq
    {
        [JsonProperty("product_id")]
        public int Product_Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image_url")]
        public string Image_Url { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("varientId")]
        public string VarientId { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }
    }

    public class ProductDetail : Product
    {
        public IEnumerable<ProductSliderImages> productImages { get; set; }
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
        public Product product { get; set; }
        public IEnumerable<MasterUnit> masterUnits { get; set; }
    }

    public class ProductSliderImages
    {
        public int? ProductSliderImageId { get; set; }
        public int ProductId { get; set; }
        public IEnumerable<IFormFile>? SliderImages{ get; set; }
        public string? Images{ get; set; }
        public string? ProductName{ get; set; }
    }
}

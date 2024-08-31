
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class MasterCategory : Image
	{
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string? CategoryImage { get; set; }
    }

    public class SubCategory : Image
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string? CategoryName { get; set; }
        public string SubCategoryDescription { get; set; }
        public string? SubCategoryImage { get; set; }
    }

    public class SubCategoryVM : SubCategory
    {
        public IEnumerable<MasterCategory>? MasterCategories { get; set; }
    }

    public class Image
    {
        public IFormFile? ImagePath{ get; set; }
    }
}

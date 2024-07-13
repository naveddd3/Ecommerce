
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

    public class Image
    {
        public IFormFile? ImagePath{ get; set; }
    }
}

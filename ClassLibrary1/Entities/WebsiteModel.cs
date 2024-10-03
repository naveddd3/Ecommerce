using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WebsiteModel
    {
        public IEnumerable<MasterCategory> Categories { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<Product>? Product { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public ProductVarientAPIRES ProductVarientAPIRES { get; set;}
    }
}

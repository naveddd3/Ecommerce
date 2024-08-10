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
        public IEnumerable<Product> Products { get; set; }

        public ProductVarientAPIRES productVarientAPIRES { get; set; }
    }
}

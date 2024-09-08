using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductVarientService
    {
        Task<dynamic> SubCategoryOnCategory(int CategoryId);
        Task<dynamic> ProductOnSubCategoryId(int ProductId);
        Task<dynamic> ProductDetailById(int ProductId);
    }
}

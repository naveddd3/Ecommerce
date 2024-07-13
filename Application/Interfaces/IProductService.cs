using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<Response> SaveOrUpdateProduct(Product product);

        Task<IEnumerable<Product>> GetProduct();
        Task<ProductVM> GetProductById(int Id);
    }
}

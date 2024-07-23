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
        Task<dynamic> GetProduct();
        Task<ProductVM> GetProductById(int LoginId,int Id);
        Task<List<ProductImage>> ShowImagesOfProduct(int ID);
        Task<Response> DeleteImageOfProduct(int Id);
    }
}

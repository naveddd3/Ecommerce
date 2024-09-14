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
        Task<ProductVM> GetProductById(int Id);
        Task<List<ProductImage>> ShowImagesOfProduct(int ID);
        Task<Response> DeleteImageOfProduct(int Id);
        Task<Response> SaveProductVarient(ProductVarientReq productVarientVM);
        Task<dynamic> GetProductVarient(int Id);
        Task<dynamic> GetProductVarientImage(int Id);
        Task<ProductVarientRes> GetProductVarientById(int Id);
        Task<Response> SaveOrUpdateProductSlider(ProductSliderImages productSliderImages);

    }
}

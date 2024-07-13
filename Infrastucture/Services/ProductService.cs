using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Services
{
    public class ProductService : IProductService
    {
        private readonly IDapperRepository _dapper;
        private readonly ICategoryService _categoryService;

        public ProductService(IDapperRepository dappers, ICategoryService categoryService)
        {
            _dapper=dappers;
            _categoryService = categoryService;
        }

        public async Task<Response> SaveOrUpdateProduct(Product product)
        {
            var res = new Response()
            {
                ResponseText = "Product Save Successfully",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                res = await _dapper.GetAsync<Response>("Proc_SaveOrUpdateProduct", new
                {
                    product.ProductId,
                    product.ProductName,
                    product.Description,
                    product.ImageUrl,
                    product.CategoryId,
                    product.MRP,
                    product.Discount
                });
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProduct()
        {
            try
            {
                var list = await _dapper.GetAllAsync<Product>("SELECT p.*,c.CategoryName FROM tbl_Products p inner join Master_Category c on p.CategoryId = c.CategoryId", null, System.Data.CommandType.Text);
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ProductVM> GetProductById(int Id)
        {
            ProductVM productVM = new ProductVM();
            try
            {

               
                var result = await _dapper.GetAsync<ProductVM>("SELECT * FROM tbl_Products WHERE ProductId = @Id", new
                {
                    Id = Id,
                }, System.Data.CommandType.Text);
                if(result != null)
                {
                    productVM = result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            productVM.MasterCategories = await _categoryService.GetCategory();
            return productVM;

        }
    }
}

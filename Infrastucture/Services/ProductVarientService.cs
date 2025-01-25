using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Services
{
    public class ProductVarientService : IProductVarientService
    {
        private readonly IDapperRepository _dapper;
        private readonly ICategoryService _categoryService;

        public ProductVarientService(IDapperRepository dappers, ICategoryService categoryService)
        {
            _dapper=dappers;
            _categoryService = categoryService;
        }

        public async Task<dynamic> SubCategoryOnCategory(int CategoryId)
        {
            try
            {
                var res = await _dapper.GetAllAsync<SubCategory>("Proc_GetSubCategoryOnCategory", new
                {
                    CategoryId
                });
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> ProductOnSubCategoryId(int SubCategoryId,decimal UserLatitude,decimal UserLongitude)
        {
            try
            {

                var product = new ProductVarientAPIRES();
                var res = await _dapper.GetMultipleAsync<Product, ProductVarientRes>("Proc_GetProductOnSubCategoryId", new {
                    SubCategoryId,
                    UserLatitude,
                    UserLongitude
                },System.Data.CommandType.StoredProcedure);
                product.Products = (List<Product>)res.GetType().GetProperty("Table1").GetValue(res, null);
                product.ProductVarients = (List<ProductVarientRes>)res.GetType().GetProperty("Table2").GetValue(res, null);
                if(product.Products.Count()<=0 && product.ProductVarients.Count() <= 0)
                {
                    product.StatusCode = ResponseStatus.Failed;
                    product.ResponseText = "No Products Found";
                }
                return product;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> ProductDetailById(int ProductId)
        {
            try
            {
                var product = new ProductDetail();
                var param = new DynamicParameters();
                param.Add("@Id", ProductId);
                var res = await _dapper.GetMultipleAsync<ProductDetail, ProductSliderImages>("PROC_GetProductDetailById", param);
                var list = (List<ProductDetail>)res.GetType().GetProperty("Table1").GetValue(res, null);
                product = list.FirstOrDefault();
                product.productImages = (List<ProductSliderImages>)res.GetType().GetProperty("Table2").GetValue(res, null);
                return product;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

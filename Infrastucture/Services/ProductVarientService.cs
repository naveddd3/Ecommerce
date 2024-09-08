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

        public async Task<dynamic> ProductOnSubCategoryId(int SubCategoryId)
        {
            try
            {
                var result = new ProductOnSubCategoryModel();
                var parameters = new DynamicParameters();
                parameters.Add("@SubCategoryId", SubCategoryId);
                var res = await _dapper.GetMultipleAsync<Product, ProductVarientRes, ProductImage>("Proc_GetProductOnSubCategoryId", parameters,System.Data.CommandType.StoredProcedure);
                result.Product = (List<Product>)res.GetType().GetProperty("Table1").GetValue(res, null);
                result.ProductVarients = (List<ProductVarientRes>)res.GetType().GetProperty("Table2").GetValue(res, null);
                result.ProductImages= (List<ProductImage>)res.GetType().GetProperty("Table3").GetValue(res, null);
                return result;

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
                var res = await _dapper.GetMultipleAsync<ProductDetail,ProductImage>("PROC_GetProductById",param);
                var list = (List<ProductDetail>)res.GetType().GetProperty("Table1").GetValue(res, null);
                product = list.FirstOrDefault();
                product.productImages = (List<ProductImage>)res.GetType().GetProperty("Table2").GetValue(res, null);
                return product;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

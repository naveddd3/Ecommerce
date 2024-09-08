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
        private readonly IUnitService _unitService;

        public ProductService(IDapperRepository dappers, ICategoryService categoryService,IUnitService unitService)
        {
            _dapper=dappers;
            _categoryService = categoryService;
            _unitService = unitService;

        }

        #region ForAdmin
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
                    product.SubCategoryId,
                    product.MRP,
                    product.Discount,
                    product.UnitId,
                    product.Quantity
                });
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<dynamic> GetProduct()
        {
            try
            {
                var list = await _dapper.GetMultipleAsync<Product, ProductImage>("PROC_GETPRODUCTS", null, System.Data.CommandType.StoredProcedure);
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ProductVM> GetProductById(int Id)
        {
            var productVM = new ProductVM();
            try
            {
                var result = await _dapper.GetMultipleAsync<Product, ProductImage>("Proc_GetProductById", new
                {
                    Id
                }, System.Data.CommandType.StoredProcedure);
                var product = (List<Product>)result.GetType().GetProperty("Table1").GetValue(result, null);
                productVM.product = product.FirstOrDefault();
                productVM.Images = (List<ProductImage>)result.GetType().GetProperty("Table2").GetValue(result, null);
            }
            catch (Exception ex)
            {

                throw;
            }
            productVM.SubCategories = await _categoryService.GetSubCategory();
            productVM.masterUnits = await _unitService.GetAll();
            return productVM;

        }
        public async Task<List<ProductImage>> ShowImagesOfProduct(int ID)
        {
            try
            {
                var res = await _dapper.GetAllAsync<ProductImage>("SELECT ProductId,ImageURI FROM ProductImages  WHERE ProductId = @ID", new
                {
                    ID = ID,
                }, System.Data.CommandType.Text);
                return res.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Response> DeleteImageOfProduct(int Id)
        {
            try
            {
                var response = await _dapper.GetAsync<Response>("Proc_DeleteProductImage", new
                {
                    Id
                });
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Response> SaveProductVarient(ProductVarientReq productVarientVM)
        {
            try
            {
                if(productVarientVM.ProductId==null|| productVarientVM.ProductId==0)
                {
                    var res = new Response() { ResponseText = "Temporary Error !",StatusCode = ResponseStatus.Failed};
                    return res;
                }
               
                var response = await _dapper.GetAsync<Response>("Proc_SaveProductVarient", new
                {
                    productVarientVM.ProductId,
                    productVarientVM.Description,
                    productVarientVM.ImageUrl,
                    productVarientVM.MRP,
                    productVarientVM.Discount,
                    productVarientVM.VarientId,
                    productVarientVM.VarientTypeId,
                    productVarientVM.ProductName,
                    productVarientVM.Quantity,
                    productVarientVM.TotalQuantity
                });
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<dynamic> GetProductVarient(int Id)
        {
            try
            {
                var list = await _dapper.GetAllAsync<ProductVarientRes>("SELECT mc.CategoryName,pv.* FROM Product_Varients pv inner join Master_Category mc on mc.CategoryId = pv.CategoryId where ProductId = @Id", new
                {
                    Id
                }, System.Data.CommandType.Text);
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<dynamic> GetProductVarientImage(int Id)
        {
            try
            {
                var list = await _dapper.GetAllAsync<ProductVarientImages>("select * from ProductVarientImages where ProductVarientId = @Id", new
                {
                    Id
                }, System.Data.CommandType.Text);
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ProductVarientRes> GetProductVarientById(int Id)
        {
            try
            {
                string query = "select * from Product_Varients where VarientId =  @Id";
                var res = await _dapper.GetAsync<ProductVarientRes>(query, new
                {
                    Id
                }, System.Data.CommandType.Text);
                return res;
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

       

    }
}

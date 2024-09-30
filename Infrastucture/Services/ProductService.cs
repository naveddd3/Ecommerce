using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        public ProductService(IDapperRepository dappers, ICategoryService categoryService, IUnitService unitService)
        {
            _dapper = dappers;
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
                var list = await _dapper.GetAllAsync<Product>("PROC_GETPRODUCTS", null, System.Data.CommandType.StoredProcedure);
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
                var result = await _dapper.GetAsync<Product>("Proc_GetProductById", new
                {
                    Id
                }, System.Data.CommandType.StoredProcedure);
                productVM.product = result;
                productVM.SubCategories = await _categoryService.GetSubCategory();
                productVM.masterUnits = await _unitService.GetAll();
            }
            catch (Exception ex)
            {

                throw;
            }
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
                if (productVarientVM.ProductId == null || productVarientVM.ProductId == 0)
                {
                    var res = new Response() { ResponseText = "Temporary Error !", StatusCode = ResponseStatus.Failed };
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
                    productVarientVM.UnitId,
                    productVarientVM.ProductName,
                    productVarientVM.Quantity,
                    productVarientVM.SubCategoryId,
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
                var list = await _dapper.GetAllAsync<ProductVarientRes>("SELECT sc.SubCategoryName,pv.* FROM Product_Varients pv inner join SubCategory sc on sc.SubCategoryId = pv.CategoryId where ProductId = @Id", new
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
        public async Task<ProductVarientRes> GetProductVarientById(int ProductId,int VarientId)
        {
            try
            {
                string proc = "Proc_GetProductVarientById";
                var res = await _dapper.GetAsync<ProductVarientRes>(proc, new
                {
                    ProductId,
					VarientId
				});
                res.masterUnits = await _unitService.GetAll();
                return res;
            }

            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Response> SaveOrUpdateProductSlider(ProductSliderImages productSliderImages)
        {
            var res = new Response()
            {
                ResponseText = "Temporary Error !",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                res = await _dapper.GetAsync<Response>("Proc_SaveProductSliderImages", new
                {
                    productSliderImages.ProductId,
                    ProductSliderImages = productSliderImages.Images,
                    productSliderImages.ProductSliderImageId
                });
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<ProductSliderImages>> GetProductSliderImagesById(int Id)
        {
            try
            {
                string query = "SELECT i.Id ProductSliderImageId,i.ProductId,i.SliderImages Images,p.ProductName ProductName FROM ProductSliderImages i INNER JOIN tbl_Products p ON p.ProductId=i.ProductId WHERE i.PRODUCTID = @Id";
                var res = await _dapper.GetAllAsync<ProductSliderImages>(query, new { Id},System.Data.CommandType.Text);
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

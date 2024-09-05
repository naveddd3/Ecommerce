﻿using Application.Interfaces;
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
                var res = await _dapper.GetMultipleAsync<ProductVarientRes, ProductVarientImages>("Proc_GetProductOnSubCategoryId", new
                {
                    SubCategoryId
                });
                return res;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

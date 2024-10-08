﻿using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
namespace Infrastucture.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDapperRepository _dapper;
        private readonly IConfiguration configuration;
        public CategoryService(IDapperRepository dapper, IConfiguration configuration)
        {
            _dapper = dapper;
            this.configuration = configuration;
        }

        public async Task<Response> SaveOrUpdateCategory(MasterCategory category)
        {
            var res = new Response()
            {
                ResponseText = "An Error is Occured !",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                res = await _dapper.GetAsync<Response>("Proc_SaveCategory", new
                {
                    category.CategoryId,
                    category.CategoryName,
                    category.CategoryDescription,
                    category.CategoryImage
                });
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MasterCategory>> GetCategory()
        {
            try
            {
                var list = await _dapper.GetAllAsync<MasterCategory>("SELECT * FROM Master_Category", null, CommandType.Text);
                var baseUrl = AppUtitlity.O.GetBaseUrl();
                if (!baseUrl.Contains("localhost"))
                {
                    var liveDomain = configuration["Domain:LiveURL"];

                    foreach (var item in list)
                    {
                        if (item.CategoryImage != null && item.CategoryImage.Contains("localhost"))
                        {
                            item.CategoryImage = item.CategoryImage.Replace("https://localhost:7035", liveDomain);
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MasterCategory> AddOrEditCategory(int Id)
        {
            var category = new MasterCategory();
            try
            {
                var res = await _dapper.GetAsync<MasterCategory>("SELECT * FROM Master_Category WHERE CategoryId = @Id", new
                {
                    Id = Id
                }, CommandType.Text);
                if (res != null)
                {
                    category = res;
                }
                return category;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Response> SaveSubCategory(SubCategory subCategory)
        {
            try
            {
                var res = await _dapper.GetAsync<Response>("Proc_SaveSubCategory", new
                {
                    subCategory.CategoryId,
                    subCategory.SubCategoryId,
                    subCategory.SubCategoryName,
                    subCategory.SubCategoryDescription,
                    subCategory.SubCategoryImage
                });
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategory()
        {
            try
            {
                var list = await _dapper.GetAllAsync<SubCategory>("SELECT SC.*,MC.CategoryName FROM SubCategory SC INNER JOIN MASTER_CATEGORY MC ON SC.CategoryId = MC.CategoryId", null, CommandType.Text);
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<SubCategoryVM> SubCategoryById(int Id)
        {
            try
            {
                var list = new SubCategoryVM();
                var res  = await _dapper.GetAsync<SubCategoryVM>("SELECT SC.*,MC.CategoryName FROM SubCategory SC INNER JOIN MASTER_CATEGORY MC ON SC.CategoryId = MC.CategoryId WHERE SubCategoryId = @Id", new
                {
                    Id
                }, CommandType.Text);
                if(res != null)
                {
                    list = res;
                }
                list.MasterCategories = await GetCategory();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System.Data;
namespace Infrastucture.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDapperRepository _dapper;
        public CategoryService(IDapperRepository dapper)
        {
            _dapper=dapper;
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
                var res  = await _dapper.GetAsync<MasterCategory>("SELECT * FROM Master_Category WHERE CategoryId = @Id", new
                {
                    Id=Id
                }, CommandType.Text);
                if(res != null)
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
    }
}

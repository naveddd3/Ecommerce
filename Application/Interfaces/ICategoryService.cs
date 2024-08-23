using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Response> SaveOrUpdateCategory(MasterCategory category);
        Task<IEnumerable<MasterCategory>> GetCategory();
        Task<MasterCategory> AddOrEditCategory(int categoryId);
        Task<SubCategory> SubCategoryById(int Id);
        Task<IEnumerable<SubCategory>> GetSubCategory();
        Task<Response> SaveSubCategory(SubCategory subCategory);
    }
}

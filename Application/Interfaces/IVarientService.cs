using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVarientService
    {
        Task<Response> SaveOrUpdateVarient(Varient varient);
        Task<Varient> AddOrEditVarient(int Id);
        Task<IEnumerable<Varient>> GetAll();
    }
}

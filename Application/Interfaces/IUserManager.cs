using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserManager
    {
        Task<ApplicationUser> FindUserAsync(string text);
        Task<ApplicationUser> FindUserByMobileAsync(string mobile);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
    }
}

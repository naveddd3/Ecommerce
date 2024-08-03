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
    public class UserManager : IUserManager
    {
        private readonly IDapperRepository _dapper;

        public UserManager(IDapperRepository dapper)
        {
            _dapper=dapper;
        }

        public async Task<ApplicationUser> FindUserAsync(string text)
        {
            try
            {
                var res = await _dapper.GetAsync<ApplicationUser>("Proc_FindUserAsync", new
                {
                    text = text
                });
                return res; 
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ApplicationUser> FindUserByMobileAsync(string mobile)
        {
            try
            {
                var res = await _dapper.GetAsync<ApplicationUser>("Proc_FindUserByMobileAsync", new
                {
                    mobile = mobile
                });
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            try
            {
                var res = await _dapper.GetAsync<ApplicationUser>("Proc_FindUserByEmailAsync", new
                {
                    email
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

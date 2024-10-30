using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Services
{
    public class SavedAddressService : ISavedAddressService
    {
        private readonly IDapperRepository _dapper;
        public SavedAddressService(IDapperRepository dapper)
        {
            _dapper = dapper;
        }

        public async Task<Response> SaveAddress(SavedAddress savedAddress)
        {
            var res = new Response()
            {
                ResponseText = "Temp Error!",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                 res = await _dapper.GetAsync<Response>("Proc_SaveOrUpdateAddress", new
                {
                    //savedAddress.AddressId,
                    //savedAddress.UserId,
                    //savedAddress.FullName,
                    //savedAddress.PhoneNumber,
                    //savedAddress.AddressLine1,
                    //savedAddress.AddressLine2,
                    //savedAddress.City,
                    //savedAddress.State,
                    //savedAddress.PostalCode
                });
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SavedAddress>> GetAddressByUserId(int UserId)
        {
            try
            {
                var res= await _dapper.GetAllAsync<SavedAddress>("SELECT * FROM SavedAddress WHERE UserId = @UserId", new
                {
                    UserId
                }, System.Data.CommandType.Text);
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

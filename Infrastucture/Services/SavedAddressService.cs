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
                 savedAddress.Id, 
                 savedAddress.UserId,
                savedAddress.Name,
                savedAddress.Type,
                savedAddress.HouseNo,
                savedAddress.Floor,
                savedAddress.Area,
                savedAddress.LandMark,
                savedAddress.PhoneNumber,
                savedAddress.City,
                savedAddress.PostalCode,
                savedAddress.OtherType 
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
                var res= await _dapper.GetAllAsync<SavedAddress>("Proc_GetUserAddressByUserId", new
                {
                    UserId
                }, System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public async Task <SavedAddress> GetAddressById(int Id)
        {
            try
            {
                var res = await _dapper.GetAsync<SavedAddress>("Proc_GetUserAddressById", new
                {
                    Id
                }, System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

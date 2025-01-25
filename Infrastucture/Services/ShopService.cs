using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;

namespace Infrastucture.Services
{
    public class ShopService : IShopService
    {
        private readonly IDapperRepository _dapper;
        public ShopService(IDapperRepository dapper)
        {
            _dapper=dapper;
        }
        public async Task<Response<IEnumerable<ShopReq>>> GetPendingShops()
        {
            var res = new Response<IEnumerable<ShopReq>>()
            {
                ResponseText = "Something has been Wrong !",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                string sp = "Proc_GetPendingShops";
                var list = await _dapper.GetAllAsync<ShopReq>(sp, null);
                if (list != null)
                {
                    res.StatusCode = ResponseStatus.Success;
                    res.ResponseText = "Found";
                    res.Result = list;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Response<IEnumerable<ShopReq>>> GetShops()
        {
            var res = new Response<IEnumerable<ShopReq>>()
            {
                ResponseText = "Something has been Wrong !",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                string sp = " SELECT * FROM Shops WHERE VerificationStatus<> 1";
                var list = await _dapper.GetAllAsync<ShopReq>(sp, null,System.Data.CommandType.Text);
                if (list != null)
                {
                    res.StatusCode = ResponseStatus.Success;
                    res.ResponseText = "Found";
                    res.Result = list;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Response> UpdateVerificationStatus(ShopVerification shopVerification)
        {
            try
            {
                string sp= "Proc_UpdateShopVerification";
                var res = await _dapper.GetAsync<Response>(sp, new
                {
                    shopVerification.ShopId,
                    shopVerification.VerificationStatus,
                    shopVerification.Remark
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

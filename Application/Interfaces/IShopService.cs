using Domain.Entities;

namespace Application.Interfaces
{
    public interface IShopService
    {
        Task<Response<IEnumerable<ShopReq>>> GetPendingShops();
        Task<Response<IEnumerable<ShopReq>>> GetShops();
        Task<Response> UpdateVerificationStatus(ShopVerification shopVerification);
    }
}

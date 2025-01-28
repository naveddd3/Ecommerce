using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Infrastucture.Services
{
    public class MapService : IMapService
    {
        private IDapperRepository _dapperRepository;

        public MapService(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        public async Task<MapModel> GetUserLocationAndDeliveryTime(double latitude, double longitude)
        {
            var response = new MapModel();
            var client = new HttpClient();
            var apiUrl = $"http://farazapi.runasp.net/api/Map/ReverseGeocodeAsync?latitude={latitude}&longitude={longitude}                        &SecretKey=zaAgkwufYHSNHOXDMvskXk1MpkTNt2RA";
            try
            {
                response.UserAddress = await client.GetStringAsync(apiUrl);
                response.DeliveryDuration = await _dapperRepository.GetAsync<string>("Proc_GetDeliveryDuration", new
                {
					UserLatitude =latitude,
					UserLongitude = longitude
				});
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

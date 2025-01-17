using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDeliveryPartnersServices 
    {
        Task<Response> RegisterDeliveryPartner(DeliveryPartner deliveryPartner);
        Task<List<DeliveryPartner>> GetAllDeliveryPartners();
        Task<DeliveryPartner> GetDeliveryPartnerById(int? id);
    }
}

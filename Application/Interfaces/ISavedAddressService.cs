using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISavedAddressService
    {
        Task<Response> SaveAddress(SavedAddress savedAddress);
        Task<IEnumerable<SavedAddress>> GetAddressByUserId(int UserId);
    }
}

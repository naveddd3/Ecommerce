using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Shop
    {
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContactNumber { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerAddress { get; set; }
        public string BusinessRegistrationNumber { get; set; }
        public string GSTNumber { get; set; }
        public string? BusinessLicensePath { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string Branch { get; set; }
        public string IFSCCode { get; set; }
        public string AdharNo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class ShopReq : Shop
    {
        public IEnumerable<IFormFile>? BusinessLicense { get; set; }
        public string? Role { get; set; } = "Vendor";
    }

    public class ShopVerification
    {
        public int ShopId { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public string Remark { get; set; }

    }
}

using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class DeliveryPartner
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? ImageUrl { get; set; }
        public string PostalCode { get; set; }
        public string VehicleType { get; set; }
        public string VehicleNumber { get; set; }
        public string AadharNumber { get; set; }
        public string DrivingLicense { get; set; }
        public string JoiningDate { get; set; }
        public string Status { get; set; } = "Active";
        public bool IsVerified { get; set; }
    }
}

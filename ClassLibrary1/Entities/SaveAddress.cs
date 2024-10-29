using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SavedAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }    
        public string HouseNo { get; set; }        
        public string? Floor { get; set; }    
        public string AddressLine1 { get; set; }    
        public string LandMark { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }            
        public string State { get; set; }           
        public string PostalCode { get; set; }  
    }

}

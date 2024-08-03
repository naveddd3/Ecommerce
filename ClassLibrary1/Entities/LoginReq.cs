using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LoginReq
    {
        [Required]
        public string EmailOrMobile { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

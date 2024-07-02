using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationRoles : IdentityRole<int>
    {
        public ApplicationRoles() : base()
        { }
        public ApplicationRoles(string name) : base(name)
        { }
    }
}

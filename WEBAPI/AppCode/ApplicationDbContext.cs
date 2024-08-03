using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace Domain.AppCodeIdentity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRoles, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


      
    }
}

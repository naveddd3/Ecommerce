using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Domain.AppCodeIdentity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRoles, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

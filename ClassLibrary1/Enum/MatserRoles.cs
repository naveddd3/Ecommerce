using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum MatserRoles
    {
        ADMIN = 1,
        USER = 2,
        VENODR = 3,
    }
    public static class MasterRole
    {
        public static string ADMIN { get; set; } = "Admin";
        public static string USER { get; set; } = "User";
        public static string VENDOR { get; set; } = "Vendor";
        public static string DPARTNER { get; set; } = "DPARTNER";
    }
}

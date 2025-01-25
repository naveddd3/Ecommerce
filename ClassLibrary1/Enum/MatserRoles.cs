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
        public const string ADMIN  = "Admin";
        public const string USER = "User";
        public const string VENDOR = "Vendor";
        public const string DPARTNER = "DPARTNER";
    }
}

using Domain.Entities;
using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Services
{
    public class ShopService
    {
        private readonly IDapperRepository _dapper;
        public ShopService(IDapperRepository dapper)
        {
            _dapper=dapper;
        }
       
    }
}

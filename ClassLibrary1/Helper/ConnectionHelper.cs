using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public interface Iconnectionstring
    {
         string Connectionstring { get; set; }
    }
    public class connectionstring : Iconnectionstring
    {
        public string Connectionstring { get; set; }
    }
}

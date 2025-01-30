using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Services
{
	public class PGFactory : IPGFactory
	{
		public async Task<dynamic> InitiateTransaction()
		{
			return "OK";
		}
	}
}

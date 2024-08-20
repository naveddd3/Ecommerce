using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public class Utitlity
    {
        public static Utitlity O { get { return Instance.Value; } }
        private static Lazy<Utitlity> Instance = new Lazy<Utitlity>(() => new Utitlity());
        private readonly IHttpContextAccessor httpContextAccessor;
        public Utitlity(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor=httpContextAccessor;
        }
        private Utitlity()
        {
        }

        public  string GetBaseUrl()
        {
            var request = httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            return baseUrl;
        }
    }
}

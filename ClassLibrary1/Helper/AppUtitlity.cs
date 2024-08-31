using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public class AppUtitlity
    {
        public static AppUtitlity O { get { return Instance.Value; } }
        private static Lazy<AppUtitlity> Instance = new Lazy<AppUtitlity>(() => new AppUtitlity());
        private AppUtitlity() { }
        public string GetBaseUrl()
        {
            string url = _GetBaseUrl();
            return url;
        }
        private HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        private string _GetBaseUrl()
        {
            var request = _httpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            return baseUrl;
        }
    }
}

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
    //public class AppUtitlity
    //{
    //    public static AppUtitlity O => instance.Value;
    //    private static Lazy<AppUtitlity> instance = new Lazy<AppUtitlity>(() => new AppUtitlity());
    //    private AppUtitlity() { }

    //    private  readonly IHttpContextAccessor _httpContextAccessor;
    //    public  void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
    //    {
    //        _httpContextAccessor = httpContextAccessor;
    //    }
    //    public string GetBaseUrl()
    //    {
    //        var request = _httpContextAccessor.HttpContext.Request;
    //        if (request == null)
    //        {
    //            throw new InvalidOperationException("HttpContext is not available.");
    //        }

    //        var baseUrl = $"{request.Scheme}://{request.Host}";
    //        return baseUrl;
    //    }
    //}


    public static class AppUtitlity
    {
        private static readonly IHttpContextAccessor httpContext;
        public static void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor;
        }


        public static string GetBaseUrl()
        {
            var request = httpContext.HttpContext.Request;
            if (request == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return baseUrl;
        }
    }

}

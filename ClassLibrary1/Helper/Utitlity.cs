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
        // Singleton instance with lazy initialization
        private static readonly Lazy<Utitlity> _instance = new Lazy<Utitlity>(() => new Utitlity());

        // Singleton property
        public static Utitlity O => _instance.Value;

        // IHttpContextAccessor dependency
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Private constructor for lazy initialization
        private Utitlity()
        {
        }

        // Public constructor for dependency injection
        public Utitlity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return baseUrl;
        }
    }

}

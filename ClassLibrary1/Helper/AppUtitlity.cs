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

        public string GenerateRandomPassword()
        {
            int length = 8;
            var random = new Random();
            var password = new StringBuilder();
            var uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var lowercaseLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var digits = "0123456789".ToCharArray();
            var specialCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?".ToCharArray();
            password.Append(uppercaseLetters[random.Next(uppercaseLetters.Length)]);
            password.Append(lowercaseLetters[random.Next(lowercaseLetters.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(specialCharacters[random.Next(specialCharacters.Length)]);
            var allCharacters = uppercaseLetters.Concat(lowercaseLetters)
                                                 .Concat(digits)
                                                 .Concat(specialCharacters)
                                                 .ToArray();

            for (int i = password.Length; i < length; i++)
            {
                password.Append(allCharacters[random.Next(allCharacters.Length)]);
            }
            return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
        }

    }
}

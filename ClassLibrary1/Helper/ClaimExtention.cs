﻿using System;
using System.Security.Claims;

namespace Domain.Helper
{
    public static class ClaimExtension
    {
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            var loggedInUserId = principal.FindFirstValue("UserId");
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }
		public static T GetLoggedInUserName<T>(this ClaimsPrincipal principal)
        {
            var loggedInUserIdClaim = principal.FindFirst("UserId");
            var loggedInUserId = loggedInUserIdClaim != null ? loggedInUserIdClaim.Value : null;

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T)) ?? default;
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : default;
            }
            else
            {
                throw new ArgumentException("Invalid type provided");
            }
        }

        public static string GetLoggedInUserToken(this ClaimsPrincipal principal)
        {
            var tokenClaim = principal.FindFirst("Token");
            return tokenClaim != null ? tokenClaim.Value : null;
        }

        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            var nameClaim = principal.FindFirst(ClaimTypes.Name);
            return nameClaim != null ? nameClaim.Value : null;
        }

        public static string GetLoggedInUserRole(this ClaimsPrincipal principal)
        {
            var roleClaim = principal.FindFirst(ClaimTypes.Role);
            return roleClaim != null ? roleClaim.Value : null;

            //if (Enum.TryParse<string>(enumValue, out var result))
            //{
            //    return result;
            //}
            //else
            //{
            //    throw new ArgumentException("Invalid role value");
            //}
        }
    }
}

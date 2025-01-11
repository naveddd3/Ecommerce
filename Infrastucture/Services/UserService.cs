using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Domain.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastucture.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRoles> roleManager;
        private readonly IConfiguration configuration;
        private readonly IDapperRepository _dapper;
        private readonly IUserManager _userManager1;
        private readonly IOTPService _oTPService;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRoles> roleManager, IConfiguration configuration, IDapperRepository dapper, IUserManager userManager1, IOTPService oTPService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            _dapper = dapper;
            _userManager1 = userManager1;
            _oTPService = oTPService;
        }

        public async Task<Response> SignUp(SignUpReq signUpReq)
        {
            var res = new Domain.Entities.Response()
            {
                ResponseText = "An Error Has Occured",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                var userexists = await userManager.FindByEmailAsync(signUpReq.Email);
                if (userexists != null)
                {
                    res.ResponseText = "User Already Exits Try With Another Email";
                    res.StatusCode = ResponseStatus.Failed;
                    return res;
                }
                ApplicationUser user = new ApplicationUser
                {
                    UserName = signUpReq.Email,
                    Email = signUpReq.Email,
                    FirstName = signUpReq.FirstName,
                    LastName = signUpReq.LastName,
                    EmailConfirmed = true,
                    MobileNo = signUpReq.MobileNo,
                };
                var result = await userManager.CreateAsync(user, signUpReq.Password);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(signUpReq.Role))
                    {
                        await roleManager.CreateAsync(new ApplicationRoles(signUpReq.Role));
                    }
                    if (await roleManager.RoleExistsAsync(signUpReq.Role))
                    {
                        await userManager.AddToRoleAsync(user, signUpReq.Role);
                        res.ResponseText = "SignUp Sucessfully";
                        res.StatusCode = ResponseStatus.Success;
                    }
                }
                else
                {
                    res.ResponseText = result.Errors.FirstOrDefault().Description;
                    res.StatusCode = ResponseStatus.Failed;
                }
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<Response<LoginResponse>> Login(LoginReq loginReq)
        {
            var response = new Domain.Entities.Response<LoginResponse>();
            try
            {

                var userexists = await _userManager1.FindUserAsync(loginReq.EmailOrMobile);
                if (userexists != null)
                {
                    var result = await userManager.CheckPasswordAsync(userexists, loginReq.Password);
                    if (!result)
                    {
                        response.ResponseText = "Password did not Match";
                        response.StatusCode = ResponseStatus.Failed;
                        return response;
                    }
                    await signInManager.SignInAsync(userexists, isPersistent: true);
                    var roledetails = await userManager.GetRolesAsync(userexists);
                    var claimlist = new List<Claim>
                       {
                        new Claim(ClaimTypes.Name, userexists.UserName),
                        new Claim("UserId", userexists.Id.ToString()),
                        new Claim(ClaimTypes.MobilePhone, userexists.MobileNo),
                        new Claim(ClaimTypes.Role, roledetails.FirstOrDefault()??""),
                      };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Secretkey"]));
                    var token = new JwtSecurityToken(
                        issuer: configuration["AuthSettings:Issuer"],
                        audience: configuration["AuthSettings:Audience"],
                        claims: claimlist,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                        );

                    string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
                    response.StatusCode = ResponseStatus.Success;
                    response.ResponseText = ResponseStatus.Success.ToString();
                    response.Result = new LoginResponse
                    {
                        Email = userexists.Email,
                        UserId = userexists.Id.ToString(),
                        UserName = userexists.UserName,
                        Name = userexists.UserName,
                        Token = tokenAsString,
                        Role = roledetails.FirstOrDefault(),
                    };
                    response.ResponseText="Login Succesfull";
                    response.StatusCode = ResponseStatus.Success;
                    return response;
                }
                response.ResponseText = "User Not Exists Please Sign Up !";
                response.StatusCode = ResponseStatus.Failed;
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Response> SaveShopOwner(ShopReq shopReq)
        {
            var res = new Response()
            {
                ResponseText = "An Error Has Occured",
                StatusCode = ResponseStatus.Failed
            };
            try
            {

                res = await _dapper.GetAsync<Response>("Proc_CheckShopIsExistOrNot", new
                {
                    shopReq.OwnerEmail, 
                    shopReq.OwnerContactNumber,
                    shopReq.BusinessRegistrationNumber,
                    shopReq.GSTNumber,
                    shopReq.AccountNumber,
                    shopReq.AdharNo,
                    shopReq.Username
                });
                if(res.StatusCode == ResponseStatus.Success)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = shopReq.Username,
                        Email = shopReq.OwnerEmail,
                        FirstName = shopReq.OwnerName,
                        LastName = shopReq.OwnerName,
                        EmailConfirmed = true,
                        MobileNo = shopReq.OwnerContactNumber,
                    };
                    var result = await userManager.CreateAsync(user, shopReq.Password);
                    if (result.Succeeded)
                    {
                        if (!await roleManager.RoleExistsAsync(shopReq.Role))
                        {
                            await roleManager.CreateAsync(new ApplicationRoles(shopReq.Role));
                        }
                        if (await roleManager.RoleExistsAsync(shopReq.Role))
                        {
                            await userManager.AddToRoleAsync(user, shopReq.Role);
                            var userexists = await userManager.FindByEmailAsync(shopReq.OwnerEmail);
                            res = await SaveShop(shopReq, userexists.Id);
                            res.ResponseText = "Shop Register Succesfull";
                            res.StatusCode = ResponseStatus.Success;
                            return res;
                        }
                    }
                }
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Response<LoginResponse>> LoginviaOTP(LoginviaOTPReq loginviaOTPReq)
        {
            var response = new Response<LoginResponse>();
            try
            {
               
                var userexists = await _userManager1.FindUserAsync(loginviaOTPReq.EmailOrMobile);
                if (userexists != null)
                {

                    await signInManager.SignInAsync(userexists, isPersistent: true);
                    var roledetails = await userManager.GetRolesAsync(userexists);
                    var claimlist = new List<Claim>
                       {
                        new Claim(ClaimTypes.Name, userexists.UserName),
                        new Claim("UserId", userexists.Id.ToString()),
                        new Claim(ClaimTypes.MobilePhone, userexists.MobileNo),
                        new Claim(ClaimTypes.Role, roledetails.FirstOrDefault()??""),
                      };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Secretkey"]));
                    var token = new JwtSecurityToken(
                        issuer: configuration["AuthSettings:Issuer"],
                        audience: configuration["AuthSettings:Audience"],
                        claims: claimlist,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                        );

                    string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
                    response.StatusCode = ResponseStatus.Success;
                    response.ResponseText = ResponseStatus.Success.ToString();
                    response.Result = new LoginResponse
                    {
                        Email = userexists.Email,
                        UserId = userexists.Id.ToString(),
                        UserName = userexists.UserName,
                        Name = userexists.UserName,
                        Token = tokenAsString,
                        Role = roledetails.FirstOrDefault(),
                    };
                    response.ResponseText = "Login Succesfull";
                    response.StatusCode = ResponseStatus.Success;
                    return response;
                }
                response.ResponseText = "User Not Exists Please Sign Up !";
                response.StatusCode = ResponseStatus.Failed;
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task<Response> SaveShop(ShopReq shop, int UserId)
        {
            var res = new Response()
            {
                ResponseText = "Error During Save Shop",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                
                res = await _dapper.GetAsync<Response>("Proc_SaveShop", new
                {
                    UserId,
                    shop.ShopName,
                    shop.ShopDescription,
                    shop.OwnerName,
                    shop.OwnerContactNumber,
                    shop.OwnerEmail,
                    shop.OwnerAddress,
                    shop.BusinessRegistrationNumber,
                    shop.GSTNumber,
                    shop.BusinessLicensePath,
                    shop.StreetAddress,
                    shop.City,
                    shop.State,
                    shop.PostalCode,
                    shop.Country,
                    shop.BankName,
                    shop.AccountNumber,
                    shop.AccountHolder,
                    shop.Branch,
                    shop.IFSCCode,
                    shop.Username,
                    shop.Password,
                    shop.AdharNo,
                    VerificationStatus = VerificationStatus.PENDING,
                    shop.Lattitude,
                    shop.Longitude
                });
                return res;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

using Application.Interfaces;
using Domain.AppCodeIdentity;
using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Infrastucture.Services;
using Microsoft.AspNetCore.Http;

namespace WEBAPI.Extension
{
    public class ServiceCollectionExtension
    {
        public static void RegisteredServices(IServiceCollection services, IConfiguration configuration)
        {
            #region identity
            string cs = configuration.GetConnectionString("Default");
            Iconnectionstring iconnectionString = new connectionstring { Connectionstring = cs };
            services.AddSingleton<Iconnectionstring>(iconnectionString);

           services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(iconnectionString.Connectionstring);
            });

            services.AddIdentity<ApplicationUser, ApplicationRoles>(
                 options =>
                 {
                     options.Password.RequireDigit = true;
                     options.Password.RequiredLength = 5;
                     options.Password.RequireLowercase = true;
                 }
                 ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["AuthSettings:Audience"],
                    ValidIssuer =configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Secretkey"])),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuickCart", Version = "v1" });

                // Configure Swagger to use JWT for authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                     });
            });
            #endregion

            #region Services
            services.AddScoped<IDapperRepository, DapperRepository>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<IProductVarientService, ProductVarientService>();
            services.AddScoped<FileUploadService>();
            #endregion
        }
    }
}

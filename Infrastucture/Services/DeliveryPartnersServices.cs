using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Domain.Helper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace Infrastucture.Services
{
    public class DeliveryPartnersServices : IDeliveryPartnersServices
    {
        private readonly IDapperRepository _dbContext;
        private readonly ILoggerService _loggerService;
        private readonly IUserService _userService;
        private readonly IUserManager _userManager;
        private readonly INotifyService _notifyService;
        public DeliveryPartnersServices(IDapperRepository dapperRepository, ILoggerService loggerService,IUserService userService, IUserManager userManager,INotifyService oTPService)
        {
            _dbContext = dapperRepository;  
            _loggerService = loggerService;
            _userService = userService;
            _userManager = userManager;
            _notifyService = oTPService;
        }

        public async Task<Response> RegisterDeliveryPartner(DeliveryPartner deliveryPartner)
        {
            var response = new Response()
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Server Down Try After Sometime !",
            };
            string proc = "Proc_SaveDeliveryPartner";
            try
            {
                string pass = AppUtitlity.O.GenerateRandomPassword();
                var res = await _userService.SignUp(new SignUpReq
                {
                    Email = deliveryPartner.Email,
                    FirstName = deliveryPartner.FirstName,
                    LastName = deliveryPartner.LastName,
                    MobileNo = deliveryPartner.PhoneNumber,
                    Role = MasterRole.DPARTNER,
                    Password = pass,
                });
                if(res.StatusCode == ResponseStatus.Success)
                {
                    var userexists = await _userManager.FindUserAsync(string.IsNullOrEmpty(deliveryPartner.Email)?deliveryPartner.PhoneNumber:deliveryPartner.Email);
                    if(userexists != null)
                    {
                        deliveryPartner.UserId = userexists.Id;
                        response = await _dbContext.GetAsync<Response>(proc, new
                        {
							deliveryPartner.UserId,
							deliveryPartner.FirstName,
                            deliveryPartner.LastName,
                            deliveryPartner.Email,
							MobileNumber=deliveryPartner.PhoneNumber,
							deliveryPartner.DrivingLicense,
							ProfilePicture=deliveryPartner.ImageUrl,
                            deliveryPartner.Address,
                            deliveryPartner.City,
                            deliveryPartner.State,
                            deliveryPartner.PostalCode,
                            deliveryPartner.VehicleType,
                            deliveryPartner.VehicleNumber,
                            deliveryPartner.AadharNumber,
                            deliveryPartner.IsVerified
                        });
                        if(response.StatusCode == ResponseStatus.Failed)
                        {
                            await _userService.DeleteUserbyId(deliveryPartner.UserId);
						}
                        _notifyService.SendCredentialViaMail(deliveryPartner.Email,pass,userexists.UserName);
                    }
                }
                else
                {
                    response.StatusCode = res.StatusCode;
                    response.ResponseText = res.ResponseText;
                }
              
            }
            catch (Exception ex)
            {
				await _userService.DeleteUserbyId(deliveryPartner.UserId);
                response.ResponseText = ex.Message;
            }
            return response;
        }
        public async Task<DeliveryPartner> GetDeliveryPartnerById(int? id)
        {
            var response = new DeliveryPartner();
            
            string query = @"
                                SELECT 
                                    ID, FirstName, LastName, Email, MobileNumber, DrivingLicense, ProfilePicture, 
                                    Address, City, State, PostalCode, VehicleType, VehicleNumber, AadharNumber, 
                                    Status 
                                FROM DeliveryPartners
                                WHERE ID = @ID;
                            ";
            try
            {
                 var result = await _dbContext.GetAsync<DeliveryPartner>(query, new
                {
                    ID = id
                }, CommandType.Text);
                if (result != null)
                {
                    response = result;
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }
        public async Task<List<DeliveryPartner>> GetAllDeliveryPartners()
        {
            var response = new List<DeliveryPartner>();

            string query = @"
                        SELECT 
                            ID,UserId, FirstName, LastName, Email, MobileNumber PhoneNumber, DrivingLicense, ProfilePicture ImageUrl, 
                            Address, City, State, PostalCode, VehicleType, VehicleNumber, AadharNumber, 
                            JoiningDate,IsVerified 
                        FROM DeliveryPartners;
                    ";
            try
            {
                var result = await _dbContext.GetAllAsync<DeliveryPartner>(query, null, CommandType.Text);
                response = result.ToList();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new ErrorLog
                {
                    StackTrace = ex.StackTrace,
                    Message = ex.Message,
                });
            }
            return response;
        }

    }
}

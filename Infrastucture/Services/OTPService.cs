using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Services
{
    public class OTPService : IOTPService
    {
        private readonly IDapperRepository _dapper;
        private readonly INotifyService _notify;
        public OTPService(IDapperRepository dapper, INotifyService notify)
        {
            _dapper=dapper;
            _notify=notify;
        }
        public async Task<Response> EmailVerificationViaOTP(VerifyEmail verifyEmail)
        {
            var res = new Response() { ResponseText = "Unable To Verify OTP !", StatusCode = ResponseStatus.Failed };
            try
            {
                if (verifyEmail.Email =="")
                {
                    res.ResponseText = "Please Provide Email.";
                }
                if (verifyEmail.OTP==null || verifyEmail.OTP == 0)
                {
                    var response = await _dapper.GetAsync<Response<int>>("Proc_VerifyEmail", new
                    {
                        verifyEmail.Email,
                        verifyEmail.OTP,
                    });
                    if (response.Result != null || response.StatusCode == ResponseStatus.Success)
                    {
                        res =  _notify.SendEmails(verifyEmail.Email,response.Result);
                        return res;
                    }
                    res.ResponseText = response.ResponseText;
                    res.StatusCode = ResponseStatus.Failed;
                    return res;
                }
                else
                {
                    var resp = await _dapper.GetAsync<Response>("Proc_VerifyEmail", new
                    {
                        verifyEmail.Email,
                        verifyEmail.OTP,
                    });
                    return resp;
                }
              
                return res;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

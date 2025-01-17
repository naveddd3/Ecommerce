using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
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
        public async Task<Response> SendOTP(OTPRequest oTPRequest)
        {
            var res = new Response() { ResponseText = "Unable To Verify OTP !", StatusCode = ResponseStatus.Failed };
            try
            {
                if (oTPRequest.Email =="")
                {
                    res.ResponseText = "Please Provide Email.";
                    return res;
                }
                if (oTPRequest.OTP==null || oTPRequest.OTP == 0)
                {
                    var response = await _dapper.GetAsync<Response<int>>("Proc_SendOTP", new
                    {
                        oTPRequest.Email,
                        oTPRequest.OTP,
                        oTPRequest.Method
                    });
                    if (response.Result != null || response.StatusCode == ResponseStatus.Success)
                    {
                        res =  _notify.SendEmails(oTPRequest.Email, response.Result);
                        return res;
                    }
                    res.ResponseText = response.ResponseText;
                    res.StatusCode = ResponseStatus.Failed;
                    return res;
                }
                else
                {
                    res.StatusCode=ResponseStatus.Failed;
                    res.ResponseText = "OTP already Sent";
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Response> MatchOTP(OTPRequest oTPRequest)
        {
            var res = new Response() { ResponseText = "Unable To Verify OTP !", StatusCode = ResponseStatus.Failed };
            try
            {
                if (oTPRequest.Email =="" || oTPRequest.OTP==null || oTPRequest.OTP == 0)
                {
                    res.ResponseText = "OTP Or Email is null";
                    return res;
                }
                var response = await _dapper.GetAsync<Response>("Proc_MatchOTP", new
                {
                    oTPRequest.Email,
                    oTPRequest.OTP,
                    oTPRequest.Method
                });
                res.ResponseText = response.ResponseText;
                res.StatusCode = response.StatusCode;
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

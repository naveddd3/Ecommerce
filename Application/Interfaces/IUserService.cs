using Domain.Entities;


namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Response> SignUp(SignUpReq signUpReq);
        Task<Response<LoginResponse>> Login(LoginReq loginReq);
        Task<Response> SaveShopOwner(ShopReq shopReq);
        Task<Response<LoginResponse>> LoginviaOTP(LoginviaOTPReq loginviaOTPReq);
        Task<Response> DeleteUserbyId(int UserId);

	}
}

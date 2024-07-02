using Domain.Entities;


namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Response> SignUp(SignUpReq signUpReq);
        Task<Response<LoginResponse>> Login(LoginReq loginReq);
    }
}

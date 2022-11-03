using Demo.DTOs;
using Demo.DTOs.User;

namespace Demo.Services.Interface
{
    public interface IUserService
    {
        Task<ErrorModel> Register(RegisterModel req);
    }
}

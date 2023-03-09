using Cleaner.Models;
using SharedLibrary.Models;

namespace Cleaner.Interfaces
{
    public interface IPlayerService    
    {
        Task<TDResponse<PlayerDTO>> GetUserById(BaseRequest<long> req);
        Task<TDResponse<PlayerDTO>> GetUserById(long id);
        Task<TDResponse<PlayerDTO>> CheckToken(BaseRequest<string> token);
        Task<TDResponse<AuthenticateResponse>> Login(BaseRequest<AuthenticateRequest> req);
        Task<TDResponse<AuthenticateResponse>> LoginWithDeviceId(BaseRequest req);
        Task<TDResponse<AuthenticateResponse>> LoginWithFacebook(BaseRequest<string> req);
        Task<TDResponse<AuthenticateResponse>> LoginWithApple(BaseRequest<string> req);
        Task<TDResponse<string>> ChangeUsername(BaseRequest<string> req, PlayerDTO player);
        Task<TDResponse> DeleteUserByUsername(BaseRequest<string> req,PlayerDTO player);
    }
}
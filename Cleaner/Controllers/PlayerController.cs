using Cleaner.Interfaces;
using Cleaner.Models;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;

namespace Cleaner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {


        private readonly ILogger<PlayerController> _logger;
        private readonly IPlayerService _playerService;

        public PlayerController(ILogger<PlayerController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        /// <summary>
        /// [LoginRequired]
        /// Get spesific user via id
        /// </summary>
        /// <param name="Data">UserId</param>
        /// <returns></returns>
        [LoginRequired]
        [HttpPost("GetUserById")]
        public async Task<TDResponse<PlayerDTO>> GetUserById([FromBody] BaseRequest<long> req)
        {
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.GetUserById(req);
        }

        [HttpPost("CheckToken")]
        public async Task<TDResponse> CheckToken([FromBody] BaseRequest<string> req)
        {
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.CheckToken(req);
        }
        

        [HttpPost("Login")]
        public async Task<TDResponse<AuthenticateResponse>> Login([FromBody] BaseRequest<AuthenticateRequest> req)
        {
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.Login(req);
        }

        [HttpPost("LoginWithDeviceId")]
        public async Task<TDResponse<AuthenticateResponse>> LoginWithDeviceId([FromBody] BaseRequest req)
        {
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.LoginWithDeviceId(req);
        }

        [HttpPost("LoginWithApple")]
        public async Task<TDResponse<AuthenticateResponse>> LoginWithApple([FromBody] BaseRequest<string> req)
        {
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.LoginWithApple(req);
        }

        [HttpPost("LoginWithFacebook")]
        public async Task<TDResponse<AuthenticateResponse>> LoginWithFacebook([FromBody] BaseRequest<string> req)
        {
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.LoginWithFacebook(req);
        }

        [LoginRequired]
        [HttpPost("ChangeUsername")]
        public async Task<TDResponse<string>> ChangeUsername([FromBody] BaseRequest<string> req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.ChangeUsername(req, user);
        }

        [LoginRequired]
        [HttpPost("DeleteUserByUsername")]
        public async Task<TDResponse> DeleteUserByUsername([FromBody] BaseRequest<string> req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _playerService.DeleteUserByUsername(req, user);
        }
    }
}
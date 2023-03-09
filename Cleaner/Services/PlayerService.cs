using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cleaner.Entities;
using Cleaner.Interfaces;
using Cleaner.Models;
using SharedLibrary.Helpers;
using SharedLibrary.Models;

namespace Cleaner.Services
{

    public class PlayerService : IPlayerService
    {


        private readonly ILogger<PlayerService> _logger;
        private readonly IMapper _mapper;
        private readonly CleanerContext _context;
        private readonly IConfiguration _configuration;

        public PlayerService(ILogger<PlayerService> logger, CleanerContext context, IMapper mapper, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<TDResponse<PlayerDTO>> GetUserById(BaseRequest<long> req)
        {
            TDResponse<PlayerDTO> response = new TDResponse<PlayerDTO>();
            var info = InfoDetail.CreateInfo(req, "GetUserById");
            var id = req.Data;
            try
            {
                var user = await _context.Player.Where(l => l.Id == id).FirstOrDefaultAsync();
                response.Data = _mapper.Map<PlayerDTO>(user);
                response.SetSuccess();
                info.AddInfo(OperationMessages.Success);
                _logger.LogInformation(info.ToString());
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
                info.SetException(e);
                _logger.LogError(info.ToString());
            }

            return response;
        }
        public async Task<TDResponse<PlayerDTO>> GetUserById(long id)
        {
            TDResponse<PlayerDTO> response = new TDResponse<PlayerDTO>();
            try
            {
                var user = await _context.Player.Where(l => l.Id == id).FirstOrDefaultAsync();
                user!.LastSeen = DateTimeOffset.UtcNow;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<PlayerDTO>(user);
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }

        public async Task<TDResponse<PlayerDTO>> CheckToken(BaseRequest<string> req)
        {

            TDResponse<PlayerDTO> response = new TDResponse<PlayerDTO>();
            var info = InfoDetail.CreateInfo(req, "CheckToken");
            try
            {
                var token = req.Data;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSECRET"));
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = long.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                response.Data = GetUserById(userId).Result.Data;
                response.SetSuccess();
                info.AddInfo(OperationMessages.Success);
                _logger.LogInformation(info.ToString());
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.TokenFail);
                info.SetException(e);
                _logger.LogError(info.ToString());
            }

            return response;
        }


        public async Task<TDResponse> DeleteUserByUsername(BaseRequest<string> req,PlayerDTO player)
        {
            TDResponse response = new TDResponse();
            try
            {
                var ent = await _context.Player.Where(l => l.Username == req.Data).FirstOrDefaultAsync();
                if (ent != null && player.Id==1)
                {
                    ent.IsActive = false;
                    await _context.SaveChangesAsync();
                    response.SetSuccess();
                }
                else
                {
                    response.SetError(OperationMessages.DbItemNotFound);
                }
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }

        public async Task<TDResponse<AuthenticateResponse>> Login(BaseRequest<AuthenticateRequest> req)
        {

            var info = InfoDetail.CreateInfo(req, "Login");

            var model = req.Data;
            TDResponse<AuthenticateResponse> response = new TDResponse<AuthenticateResponse>();
            try
            {
                var userEnt = await _context.Player.FirstOrDefaultAsync(x => x.Username == model.Username && x.IsActive == true);

                if (userEnt?.PasswordHash?.Equals(HashHelper.ComputeSha256Hash(model.Password)) != true)
                {
                    response.SetError(OperationMessages.AuthenticateError);
                    info.AddInfo(OperationMessages.AuthenticateError);
                    _logger.LogInformation(info.ToString());
                }
                else
                {
                    var user = _mapper.Map<Player, PlayerDTO>(userEnt);
                    var token = generateJwtToken(user);
                    response.Data = new AuthenticateResponse(user, token);
                    response.SetSuccess();
                    info.AddInfo(OperationMessages.Success);
                    _logger.LogInformation(info.ToString());

                }

            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
                info.SetException(e);
                _logger.LogError(info.ToString());
            }

            return response;
        }

        public async Task<TDResponse<AuthenticateResponse>> LoginWithDeviceId(BaseRequest req)
        {

            var info = InfoDetail.CreateInfo(req, "LoginWithDeviceId");

            TDResponse<AuthenticateResponse> response = new TDResponse<AuthenticateResponse>();
            try
            {
                var userEnt = await _context.Player.Where(x => x.MobileUserId == req.Info.DeviceId && x.IsActive == true).FirstOrDefaultAsync();
                if (userEnt == null)
                {
                    await _context.AddAsync(new Player()
                    {
                        IsActive = true,
                        IsAndroid = false,
                        Email = "",
                        FirstLogInDate = DateTimeOffset.UtcNow,
                        LastSeen = DateTimeOffset.UtcNow,
                        PasswordHash = null,
                        MobileUserId = req.Info.DeviceId,
                        Username = "",
                    });
                    await _context.SaveChangesAsync();
                    userEnt = await _context.Player.Where(x => x.MobileUserId == req.Info.DeviceId && x.IsActive == true).FirstOrDefaultAsync();
                    userEnt.Username = "user_" + userEnt.Id;
                    userEnt.Email = "user_" + userEnt.Id;
                    await _context.SaveChangesAsync();
                }


                var user = _mapper.Map<Player, PlayerDTO>(userEnt);
                var token = generateJwtToken(user);
                response.Data = new AuthenticateResponse(user, token);
                response.SetSuccess();
                info.AddInfo(OperationMessages.Success);
                _logger.LogInformation(info.ToString());


            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
                info.SetException(e);
                _logger.LogError(info.ToString());
            }

            return response;
        }

        public async Task<TDResponse<string>> ChangeUsername(BaseRequest<string> req,PlayerDTO player)
        {

            var info = InfoDetail.CreateInfo(req, "ChangeUsername");

            TDResponse<string> response = new TDResponse<string>();
            try
            {
                var userEnt = await _context.Player.Where(x => x.Id == player.Id && x.IsActive == true).FirstOrDefaultAsync();
                var userExist = await _context.Player.Where(l => l.Username == req.Data && l.IsActive==true).FirstOrDefaultAsync();
                if (userExist!=null || userEnt == null)
                {
                    response.SetError(OperationMessages.DuplicateRecord);
                    info.AddInfo(OperationMessages.DuplicateRecord);
                    _logger.LogInformation(info.ToString());
                    return response;
                }
                userEnt.Username = req.Data;
                await _context.SaveChangesAsync();
                response.Data = userEnt.Username;
                response.SetSuccess(OperationMessages.ChangeNameSuccess);
                info.AddInfo(OperationMessages.ChangeNameSuccess);
                _logger.LogInformation(info.ToString());


            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
                info.SetException(e);
                _logger.LogError(info.ToString());
            }

            return response;
        }

        public async Task<TDResponse<AuthenticateResponse>> LoginWithApple(BaseRequest<string> req)
        {

            var info = InfoDetail.CreateInfo(req, "LoginWithApple");

            TDResponse<AuthenticateResponse> response = new TDResponse<AuthenticateResponse>();
            try
            {
                var userEnt = await _context.Player.Where(x => x.AppleId == req.Data && x.IsActive == true).FirstOrDefaultAsync();
                if (userEnt == null)
                {
                    var deviceUser = await _context.Player.Where(x => x.MobileUserId == req.Info.DeviceId && x.IsActive == false).FirstOrDefaultAsync();
                    if (deviceUser == null)
                    {

                        await _context.AddAsync(new Player()
                        {
                            IsActive = true,
                            IsAndroid = false,
                            Email = "",
                            AppleId = req.Data,
                            FacebookId = null,
                            GooglePlayId = null,
                            FirstLogInDate = DateTimeOffset.UtcNow,
                            LastSeen = DateTimeOffset.UtcNow,
                            PasswordHash = null,
                            MobileUserId = null,
                            Username = "",
                        });
                        await _context.SaveChangesAsync();
                        userEnt = await _context.Player.Where(x => x.AppleId == req.Data && x.IsActive == true).FirstOrDefaultAsync();
                        userEnt.Username = "user_" + userEnt.Id;
                        userEnt.Email = "user_" + userEnt.Id;
                    }
                    else
                    {
                        deviceUser.IsActive = true;
                        deviceUser.AppleId = req.Data;
                        deviceUser.MobileUserId = null;
                    }
                    await _context.SaveChangesAsync();
                    userEnt = await _context.Player.Where(x => x.AppleId == req.Data && x.IsActive == true).FirstOrDefaultAsync();
                }

                var user = _mapper.Map<Player, PlayerDTO>(userEnt);
                var token = generateJwtToken(user);
                response.Data = new AuthenticateResponse(user, token);
                response.SetSuccess();
                info.AddInfo(OperationMessages.Success);
                _logger.LogInformation(info.ToString());


            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
                info.SetException(e);
                _logger.LogError(info.ToString());
            }

            return response;
        }

        public async Task<TDResponse<AuthenticateResponse>> LoginWithFacebook(BaseRequest<string> req)
        {

            var info = InfoDetail.CreateInfo(req, "LoginWithFacebook");

            TDResponse<AuthenticateResponse> response = new TDResponse<AuthenticateResponse>();
            try
            {
                var userEnt = await _context.Player.Where(x => x.FacebookId == req.Data && x.IsActive == true).FirstOrDefaultAsync();
                if (userEnt == null)
                {
                    var deviceUser = await _context.Player.Where(x => x.MobileUserId == req.Info.DeviceId && x.IsActive == false).FirstOrDefaultAsync();
                    if (deviceUser == null)
                    {

                        await _context.AddAsync(new Player()
                        {
                            IsActive = true,
                            IsAndroid = false,
                            Email = "",
                            AppleId = null,
                            FacebookId = req.Data,
                            GooglePlayId = null,
                            FirstLogInDate = DateTimeOffset.UtcNow,
                            LastSeen = DateTimeOffset.UtcNow,
                            PasswordHash = null,
                            MobileUserId = null,
                            Username = "",
                        });
                        await _context.SaveChangesAsync();
                        userEnt = await _context.Player.Where(x => x.FacebookId == req.Data && x.IsActive == true).FirstOrDefaultAsync();
                        userEnt.Username = "user_" + userEnt.Id;
                        userEnt.Email = "user_" + userEnt.Id;
                    }
                    else
                    {
                        deviceUser.IsActive = true;
                        deviceUser.FacebookId = req.Data;
                        deviceUser.MobileUserId = null;
                    }
                    await _context.SaveChangesAsync();
                    userEnt = await _context.Player.Where(x => x.FacebookId == req.Data && x.IsActive == true).FirstOrDefaultAsync();
                }

                var user = _mapper.Map<Player, PlayerDTO>(userEnt);
                var token = generateJwtToken(user);
                response.Data = new AuthenticateResponse(user, token);
                response.SetSuccess();
                info.AddInfo(OperationMessages.Success);
                _logger.LogInformation(info.ToString());


            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
                info.SetException(e);
                _logger.LogError(info.ToString());
            }

            return response;
        }

        private string generateJwtToken(PlayerDTO player)
        {
            // generate token that is valid for 23 HOURS
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSECRET"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", player.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(23),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}
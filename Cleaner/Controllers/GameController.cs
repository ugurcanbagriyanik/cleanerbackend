using Cleaner.Interfaces;
using Cleaner.Models;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;

namespace Cleaner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {

        private readonly ILogger<GameController> _logger;
        private readonly IGameService _service;

        public GameController(ILogger<GameController> logger, IGameService service)
        {
            _logger = logger;
            _service = service;
        }
        
        
        /// <summary>
        /// playera ait body partlari doner
        /// </summary>
        /// <remarks>
        /// ### DETAILS ###
        /// <br/>
        /// <br/>
        /// Input: BaseRequest
        /// <br/>
        /// Output: TDResponse &lt; List &lt; PlayerBodyPartDTO &gt; &gt;
        /// </remarks>Task<TDResponse<List<PlayerBodyPartDTO>>> GetPlayerBodyParts(BaseRequest req, PlayerDTO player)
        [LoginRequired]
        [HttpPost("GetPlayerBodyParts")]
        public async Task<TDResponse<List<PlayerBodyPartDTO>>> GetPlayerBodyParts([FromBody] BaseRequest req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _service.GetPlayerBodyParts(req, user);
        }        
        
        /// <summary>
        /// playera ait bodyleri doner
        /// </summary>
        /// <remarks>
        /// ### DETAILS ###
        /// <br/>
        /// <br/>
        /// Input: BaseRequest
        /// <br/>
        /// Output: TDResponse &lt; List &lt; PlayerCleanerDTO &gt; &gt;
        /// </remarks>
        [LoginRequired]
        [HttpPost("GetPlayerBodies")]
        public async Task<TDResponse<List<PlayerCleanerDTO>>> GetPlayerBodies([FromBody] BaseRequest req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _service.GetPlayerBodies(req, user);
        }
        
                
        /// <summary>
        /// playera ait ayarlanmis warmachine i doner
        /// </summary>
        /// <remarks>
        /// ### DETAILS ###
        /// <br/>
        /// <br/>
        /// Input: BaseRequest
        /// <br/>
        /// Output: TDResponse &lt; PlayerCleanerDTO &gt;
        /// </remarks>
        [LoginRequired]
        [HttpPost("GetPlayerWarMachine")]
        public async Task<TDResponse<PlayerWarMachineDTO>> GetPlayerWarMachine([FromBody] BaseRequest req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _service.GetPlayerWarMachine(req, user);
        }

        /// <summary>
        /// random bir enemy araci getirir
        /// </summary>
        /// <remarks>
        /// ### DETAILS ###
        /// <br/>
        /// <br/>
        /// Input: BaseRequest
        /// <br/>
        /// Output: TDResponse  &lt; EnemyWarMachineDTO &gt;
        /// </remarks>
        [LoginRequired]
        [HttpPost("GetEnemyWarMachine")]
        public async Task<TDResponse<EnemyWarMachineDTO>> GetEnemyWarMachine([FromBody] BaseRequest req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _service.GetEnemyWarMachine(req, user);
        }

        /// <summary>
        /// playerin 
        /// </summary>
        /// <remarks>
        /// ### DETAILS ###
        /// <br/>
        /// <br/>
        /// Input: BaseRequest &lt; SetWarMachineReq &gt;
        /// <br/>
        /// Output: TDResponse
        /// </remarks>
        [LoginRequired]
        [HttpPost("SetPlayerWarMachine")]
        public async Task<TDResponse> SetPlayerWarMachine([FromBody] BaseRequest<SetWarMachineReq> req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _service.SetPlayerWarMachine(req, user);
        }
                     
        /// <summary>
        /// verilen rarity'e gore playera random bir body part ekler, eklenen bodyparti doner
        /// </summary>
        /// <remarks>
        /// ### DETAILS ###
        /// <br/>
        /// <br/>
        /// Input: BaseRequest &lt; int &gt; //Not:Rarity
        /// <br/>
        /// Output: TDResponse  &lt; PlayerBodyPartDTO &gt;
        /// </remarks>
        [LoginRequired]
        [HttpPost("GetBodyPartChest")]
        public async Task<TDResponse<PlayerBodyPartDTO>> GetBodyPartChest([FromBody] BaseRequest<int> req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _service.GetBodyPartChest(req, user);
        }
                             
        /// <summary>
        /// verilen rarity'e gore playera random bir cleaner ekler, eklenen clenaeri doner
        /// </summary>
        /// <remarks>
        /// ### DETAILS ###
        /// <br/>
        /// <br/>
        /// Input: BaseRequest &lt; int &gt; //Not:Rarity
        /// <br/>
        /// Output: TDResponse  &lt; PlayerCleanerDTO &gt;
        /// </remarks>
        [LoginRequired]
        [HttpPost("GetCleanerChest")]
        public async Task<TDResponse<PlayerCleanerDTO>> GetCleanerChest([FromBody] BaseRequest<int> req)
        {
            var user = (HttpContext.Items["User"] as PlayerDTO);
            req.SetUser(user.Id);
            req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
            return await _service.GetCleanerChest(req, user);
        }
        

    }
}

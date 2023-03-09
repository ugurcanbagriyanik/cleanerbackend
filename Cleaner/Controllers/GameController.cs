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
        
        //
        // /// <summary>
        // /// Chapterin genel infosunu doner
        // /// </summary>
        // /// <remarks>
        // /// ### DETAILS ###
        // /// <br/>
        // /// <br/>
        // /// Input: BaseRequest
        // /// <br/>
        // /// Output: TDResponse &lt; ChapterInfoDTO &gt;
        // /// </remarks>
        // [LoginRequired]
        // [HttpPost("GetChapterInfo")]
        // public async Task<TDResponse> GetChapterInfo([FromBody] BaseRequest req)
        // {
        //     var user = (HttpContext.Items["User"] as PlayerDto);
        //     req.SetUser(user.Id);
        //     req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
        //     return await _service.GetChapterInfo(req, user);
        // }
        //
        // /// <summary>
        // /// playerin research durumunu doner
        // /// </summary>
        // /// <remarks>
        // /// ### DETAILS ###
        // /// <br/>
        // /// <br/>
        // /// Input: BaseRequest
        // /// <br/>
        // /// Output: TDResponse &lt; List &lt; PlayerResearchNodeLevelDTO &gt; &gt;
        // /// </remarks>
        // [LoginRequired]
        // [HttpPost("GetPlayerResearchNodeLevels")]
        // public async Task<TDResponse<List<PlayerResearchNodeLevelDTO>>> GetPlayerResearchNodeLevels([FromBody] BaseRequest req)
        // {
        //     var user = (HttpContext.Items["User"] as PlayerDto);
        //     req.SetUser(user.Id);
        //     req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
        //     return await _service.GetPlayerResearchNodeLevels(req, user);
        // }
        //
        // /// <summary>
        // /// playerin research durumunu setler ve doner
        // /// </summary>
        // /// <remarks>
        // /// ### DETAILS ###
        // /// <br/>
        // /// <br/>
        // /// Input: BaseRequest &lt; List &lt; PlayerResearchNodeLevelDTO &gt; &gt;
        // /// <br/>
        // /// Output: TDResponse &lt; List &lt; PlayerResearchNodeLevelDTO &gt; &gt;
        // /// </remarks>
        // [LoginRequired]
        // [HttpPost("SetPlayerResearchNodeLevels")]
        // public async Task<TDResponse<List<PlayerResearchNodeLevelDTO>>> SetPlayerResearchNodeLevels([FromBody] BaseRequest<List<PlayerResearchNodeLevelDTO>> req)
        // {
        //     var user = (HttpContext.Items["User"] as PlayerDto);
        //     req.SetUser(user.Id);
        //     req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
        //     return await _service.SetPlayerResearchNodeLevels(req, user);
        // }
        //
        // /// <summary>
        // /// Progress eklemek icin kullanilir
        // /// </summary>
        // /// <remarks>
        // /// ### DETAILS ###
        // /// <br/>
        // /// <br/>
        // /// Input: BaseRequest &lt; List &lt; ProgressDTO &gt; &gt;
        // /// <br/>
        // /// Output: TDResponse 
        // /// </remarks>
        // [LoginRequired]
        // [HttpPost("AddProgressList")]
        // public async Task<TDResponse> AddProgressList([FromBody] BaseRequest<List<ProgressDTO>> req)
        // {
        //     var user = (HttpContext.Items["User"] as PlayerDto);
        //     req.SetUser(user.Id);
        //     req.SetIp(HttpContext.Connection.RemoteIpAddress?.ToString());
        //     return await _service.AddProgressList(req, user);
        // }
        //
        //
        
        

    }
}
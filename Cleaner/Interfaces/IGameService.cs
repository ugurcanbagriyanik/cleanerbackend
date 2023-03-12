#pragma warning disable CS1591
using Cleaner.Models;
using SharedLibrary.Models;

namespace Cleaner.Interfaces
{
    public interface IGameService    
    {
        Task<TDResponse<List<PlayerBodyPartDTO>>> GetPlayerBodyParts(BaseRequest req, PlayerDTO player);
        Task<TDResponse<List<PlayerCleanerDTO>>> GetPlayerBodies(BaseRequest req, PlayerDTO player);
        Task<TDResponse<PlayerWarMachineDTO>> GetPlayerWarMachine(BaseRequest req, PlayerDTO player);
        Task<TDResponse<EnemyWarMachineDTO>> GetEnemyWarMachine(BaseRequest req, PlayerDTO player);
        Task<TDResponse> SetPlayerWarMachine(BaseRequest<SetWarMachineReq> req, PlayerDTO player);
    }
}
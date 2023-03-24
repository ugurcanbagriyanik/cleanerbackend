using AutoMapper;
using Cleaner.Entities;
using Cleaner.Enums;
using Cleaner.Interfaces;
using Cleaner.Models;
using SharedLibrary.Helpers;
using SharedLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Cleaner.Services
{

    public class GameService : IGameService
    {


        private readonly ILogger<GameService> _logger;
        private readonly IMapper _mapper;
        private readonly CleanerContext _context;
        private readonly IConfiguration _configuration;

        public GameService(ILogger<GameService> logger, CleanerContext context, IMapper mapper, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        // public async Task<TDResponse<ChapterInfoDTO>> GetChapterInfo(BaseRequest req, PlayerDto player)
        // {
        //         TDResponse<ChapterInfoDTO> response = new TDResponse<ChapterInfoDTO>();
        //         var info = InfoDetail.CreateInfo(req, "GetPlayerChapterInfo");
        //
        //         try
        //         {
        //             var userStatus =await _context.UserTdStatus.Include(l=>l.Level).Where(l => l.UserId == player.Id).FirstOrDefaultAsync();
        //             var chapterId = 1;
        //             if (userStatus?.TdLevelId !=null)
        //             {
        //                 chapterId = userStatus.Level.ChapterId;
        //             }
        //             var query = _context.Chapter.Include(l => l.Levels)
        //                 .Where(l => l.Id == chapterId);
        //             var chapter = await _mapper.ProjectTo<ChapterInfoDTO>(query).FirstOrDefaultAsync();
        //
        //             var userLevels = await _context.UserProgressHistory
        //                 .Where(l => l.UserId == player.Id && l.GainedStar > 0 && l.Level.ChapterId == chapter.Id)
        //                 .GroupBy(l=>l.LevelId)
        //                 .Select(l=>l.OrderByDescending(x=>x.GainedStar).First())
        //                 .ToListAsync();
        //
        //             foreach (var o in userLevels)
        //             {
        //                 var cc = chapter.Levels.FirstOrDefault(l => l.Id == o.LevelId);
        //                 if (cc!=null)
        //                 {
        //                     cc.UserStar = o.GainedStar;
        //                 }
        //             }
        //             
        //             response.Data = chapter;
        //             response.SetSuccess();
        //             info.AddInfo(OperationMessages.Success);
        //             _logger.LogInformation(info.ToString());
        //             await SyncPlayerTable( new []{
        //                 TableEnum.Chapter
        //             }, player);
        //         }
        //         catch (Exception e)
        //         {
        //             response.SetError(OperationMessages.DbError);
        //             info.SetException(e);
        //             _logger.LogError(info.ToString());
        //         }
        //
        //         return response;
        // }
        //
        public async Task<TDResponse<List<PlayerBodyPartDTO>>> GetPlayerBodyParts(BaseRequest req, PlayerDTO player)
        {
            TDResponse<List<PlayerBodyPartDTO>> response = new TDResponse<List<PlayerBodyPartDTO>>();
            var info = InfoDetail.CreateInfo(req, "GetPlayerBodyParts");
            try
            {
                var query = _context.PlayerBodyPart
                    .Include(l=>l.GeneratableBodyPart)
                    .Where(l=>l.PlayerId==player.Id && l.IsActive).OrderBy(l=>l.GeneratableBodyPart.BodyPartTypeEnumId).ThenBy(l=>l.GeneratableBodyPart.Rarity);
                var playerBodyParts = await _mapper.ProjectTo<PlayerBodyPartDTO>(query).ToListAsync();
                
                response.Data = playerBodyParts;
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
        
        public async Task<TDResponse<List<PlayerCleanerDTO>>> GetPlayerBodies(BaseRequest req, PlayerDTO player)
        {
            TDResponse<List<PlayerCleanerDTO>> response = new TDResponse<List<PlayerCleanerDTO>>();
            var info = InfoDetail.CreateInfo(req, "GetPlayerBodies");
            try
            {
                var query = _context.PlayerCleaner
                    .Include(l=>l.GeneratableCleaner)
                    .Where(l=>l.PlayerId==player.Id && l.IsActive).OrderBy(l=>l.GeneratableCleaner.Rarity).ThenBy(l=>l.GeneratableCleaner.DefaultBattery);
                var playerBodies = await _mapper.ProjectTo<PlayerCleanerDTO>(query).ToListAsync();
                
                response.Data = playerBodies;
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
        
        public async Task<TDResponse<PlayerWarMachineDTO>> GetPlayerWarMachine(BaseRequest req, PlayerDTO player)
        {
            TDResponse<PlayerWarMachineDTO> response = new TDResponse<PlayerWarMachineDTO>();
            var info = InfoDetail.CreateInfo(req, "GetPlayerWarMachine");
            try
            {
                var query = _context.PlayerWarMachine
                    .Include(l => l.PlayerCleaner)
                    .Include(l => l.PlayerWarMachineParts)
                    .Where(l => l.PlayerCleaner.PlayerId == player.Id && l.IsActive);
                var playerWarMachineDto = await _mapper.ProjectTo<PlayerWarMachineDTO>(query).FirstOrDefaultAsync();
                
                response.Data = playerWarMachineDto;
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
        
        public async Task<TDResponse<EnemyWarMachineDTO>> GetEnemyWarMachine(BaseRequest req, PlayerDTO player)
        {
            TDResponse<EnemyWarMachineDTO> response = new TDResponse<EnemyWarMachineDTO>();
            var info = InfoDetail.CreateInfo(req, "GetEnemyWarMachine");
            try
            {
                var query = _context.PlayerWarMachine
                    .Include(l => l.PlayerCleaner)
                    .ThenInclude(l=>l.Player)
                    .Include(l => l.PlayerWarMachineParts)
                    .Where(l => l.PlayerCleaner.PlayerId != player.Id && l.IsActive).OrderBy(r => Guid.NewGuid());
                var enemyWarMachineDto = await _mapper.ProjectTo<EnemyWarMachineDTO>(query).FirstOrDefaultAsync();
                
                response.Data = enemyWarMachineDto;
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
        
        public async Task<TDResponse> SetPlayerWarMachine(BaseRequest<SetWarMachineReq> req, PlayerDTO player)
        {
            TDResponse response = new TDResponse();
            var info = InfoDetail.CreateInfo(req, "SetPlayerWarMachine");
        
            try
            {
                if (req.DataIsNullOrEmpty() || req.Data!.PlayerWarMachineParts.IsNullOrEmpty())
                {
                    info.AddInfo(OperationMessages.InputError);
                    response.SetError(OperationMessages.InputError);
                    _logger.LogInformation(info.ToString());
                    return response;
                }

                var q = await _context.PlayerWarMachine.Include(l => l.PlayerWarMachineParts)
                    .Where(l => l.IsActive && l.PlayerCleaner.PlayerId == player.Id).FirstOrDefaultAsync();
                if (q != null)
                {
                    q.PlayerWarMachineParts.ForEach(l=>l.IsActive=false);
                    q.IsActive = false;
                }
                

                await _context.SaveChangesAsync();

                var entWarMachine = new PlayerWarMachine()
                {
                    
                    Path = req.Data.Path,
                    IsActive = false,
                    PlayerCleanerId = req.Data.PlayerCleanerId
                };
                await _context.AddAsync(entWarMachine);
                await _context.SaveChangesAsync();

                var entWarMachineParts = req.Data.PlayerWarMachineParts.Select(l => new PlayerWarMachinePart()
                {
                    HolderId = l.HolderId,
                    IsActive = true,
                    PlayerBodyPartId = l.PlayerBodyPartId,
                    PlayerWarMachineId = entWarMachine.Id
                }).ToList();
                await _context.AddRangeAsync(entWarMachineParts);
                entWarMachine.IsActive = true;
                await _context.SaveChangesAsync();
                
                
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
        
        
                
        public async Task<TDResponse<PlayerCleanerDTO>> GetCleanerChest(BaseRequest<int> req, PlayerDTO player)
        {
            TDResponse<PlayerCleanerDTO> response = new TDResponse<PlayerCleanerDTO>();
            var info = InfoDetail.CreateInfo(req, "GetBodyChest");
            try
            {
                
                var randomPlayerCleaner =await _context.GeneratableCleaner.Where(l=>l.Rarity==req.Data).OrderBy(r => Guid.NewGuid()).FirstOrDefaultAsync();
                
                var ent = new PlayerCleaner()
                {
                    PlayerId = player.Id,
                    IsActive = true,
                    GeneratableCleanerId = randomPlayerCleaner?.Id ?? 1
                };
                
                
                await _context.AddAsync(ent);
                await _context.SaveChangesAsync();
                
                


                response.Data =await _mapper.ProjectTo<PlayerCleanerDTO>(_context.PlayerCleaner.Where(l=>l.Id==ent.Id)).FirstOrDefaultAsync();
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
        
        
        public async Task<TDResponse<PlayerBodyPartDTO>> GetBodyPartChest(BaseRequest<int> req, PlayerDTO player)
        {
            TDResponse<PlayerBodyPartDTO> response = new TDResponse<PlayerBodyPartDTO>();
            var info = InfoDetail.CreateInfo(req, "GetBodyPartChest");
            try
            {
                
                var randomBodyPart =await _context.GeneratableBodyPart.Where(l=>l.Rarity==req.Data).OrderBy(r => Guid.NewGuid()).FirstOrDefaultAsync();
                
                var ent = new PlayerBodyPart()
                {
                    PlayerId = player.Id,
                    IsActive = true,
                    GeneratableBodyPartId = randomBodyPart?.Id ?? 1
                };
                
                
                await _context.AddAsync(ent);
                await _context.SaveChangesAsync();
                
                


                response.Data =await _mapper.ProjectTo<PlayerBodyPartDTO>(_context.PlayerBodyPart.Where(l=>l.Id==ent.Id)).FirstOrDefaultAsync();
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
        
    }
}
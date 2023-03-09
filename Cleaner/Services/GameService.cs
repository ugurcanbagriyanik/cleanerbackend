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
        // public async Task<TDResponse<List<PlayerResearchNodeLevelDTO>>> GetPlayerResearchNodeLevels(BaseRequest req, PlayerDto player)
        // {
        //     TDResponse<List<PlayerResearchNodeLevelDTO>> response = new TDResponse<List<PlayerResearchNodeLevelDTO>>();
        //     var info = InfoDetail.CreateInfo(req, "GetPlayerResearchNodeLevels");
        //     try
        //     {
        //         var query = _context.PlayerResearchNodeLevel;
        //         var playerResearchNodeDtos = await _mapper.ProjectTo<PlayerResearchNodeLevelDTO>(query).ToListAsync();
        //         
        //         response.Data = playerResearchNodeDtos;
        //         response.SetSuccess();
        //         info.AddInfo(OperationMessages.Success);
        //         _logger.LogInformation(info.ToString());
        //     }
        //     catch (Exception e)
        //     {
        //         response.SetError(OperationMessages.DbError);
        //         info.SetException(e);
        //         _logger.LogError(info.ToString());
        //     }
        //
        //     return response;
        // }
        //
        // public async Task<TDResponse<List<PlayerResearchNodeLevelDTO>>> SetPlayerResearchNodeLevels(BaseRequest<List<PlayerResearchNodeLevelDTO>> req, PlayerDto player)
        // {
        //     TDResponse<List<PlayerResearchNodeLevelDTO>> response = new TDResponse<List<PlayerResearchNodeLevelDTO>>();
        //     var info = InfoDetail.CreateInfo(req, "SetPlayerResearchNodeLevels");
        //     try
        //     {
        //         if (req.Data==null)
        //         {
        //             response.SetError(OperationMessages.InputError);
        //             info.AddInfo(OperationMessages.InputError);
        //             _logger.LogInformation(info.ToString());
        //             return response;
        //         }
        //         
        //         foreach (var ld in req.Data)
        //         {
        //             if (ld.Id==0)
        //             {
        //                 await _context.AddAsync(new PlayerResearchNodeLevel()
        //                 {
        //                     UserId = player.Id,
        //                     ResearchNodeLevelId = ld.ResearchNodeLevelId
        //                 });
        //             }
        //             else
        //             {
        //                 var ent = await _context.PlayerResearchNodeLevel.Where(l => l.UserId == player.Id && l.Id == ld.Id)
        //                     .FirstOrDefaultAsync();
        //                 if (ent != null)
        //                 {
        //                     ent.ResearchNodeLevelId = ld.ResearchNodeLevelId;
        //                 }
        //
        //             }
        //             
        //             await _context.SaveChangesAsync();
        //
        //         }
        //         
        //         response.SetSuccess();
        //         info.AddInfo(OperationMessages.Success);
        //         _logger.LogInformation(info.ToString());
        //     }
        //     catch (Exception e)
        //     {
        //         response.SetError(OperationMessages.DbError);
        //         info.SetException(e);
        //         _logger.LogError(info.ToString());
        //     }
        //
        //     return response;
        // }
        //
        // public async Task<TDResponse> AddProgressList(BaseRequest<List<ProgressDTO>> req, PlayerDto player)
        // {
        //     TDResponse response = new TDResponse();
        //     var info = InfoDetail.CreateInfo(req, "AddProgressList");
        //
        //     try
        //     {
        //         if (req.Data==null)
        //         {
        //             info.AddInfo(OperationMessages.InputError);
        //             response.SetError(OperationMessages.InputError);
        //             _logger.LogInformation(info.ToString());
        //             return response;
        //         }
        //         foreach (var p in req.Data)
        //         {
        //             var userProgressHistory = new UserProgressHistory();
        //             userProgressHistory.UserId = player.Id;
        //             userProgressHistory.LevelId = p.LevelId;
        //             userProgressHistory.GainedStar = p.StarCount;
        //             userProgressHistory.BarrierHealth = p.BarrierHealth;
        //             userProgressHistory.GainedCoin = p.GainedCoin;
        //             userProgressHistory.SpentCoin = p.SpentCoin;
        //             userProgressHistory.TotalCoin = p.TotalCoin;
        //             userProgressHistory.WaveStartTime = p.WaveStartTime.ToDateTimeOffsetUtc() ?? DateTimeOffset.UtcNow;
        //             userProgressHistory.WaveEndTime = userProgressHistory.WaveStartTime + TimeSpan.FromSeconds(p.TimeSecond);
        //             await _context.AddAsync(userProgressHistory);
        //             await _context.SaveChangesAsync();
        //             await _context.AddRangeAsync(p.EnemyKillList.Select(l => new EnemyKill()
        //             {
        //                 UserProgressHistoryId = userProgressHistory.Id,
        //                 DeadCount = l.DeadCount,
        //                 EnemyLevelId = l.EnemyLevelId
        //             }).ToList());
        //             await _context.SaveChangesAsync();
        //             await _context.AddRangeAsync(p.TowerProgressList.Select(l=> new TowerProgress()
        //             {
        //                 UserProgressHistoryId = userProgressHistory.Id,
        //                 TowerId = l.TowerId,
        //                 TowerCount = l.TowerCount,
        //                 TowerDamage = l.TowerDamage,
        //                 TowerArmorDamage = l.TowerArmorDamage,
        //                 TowerDotDamage = l.TowerDotDamage,
        //                 TowerFireCount = l.TowerFireCount,
        //                 TowerUpgradeNumber = l.TowerUpgradeNumber
        //             }).ToList());
        //             await _context.SaveChangesAsync();
        //
        //         }
        //         
        //         response.SetSuccess();
        //         info.AddInfo(OperationMessages.Success);
        //         _logger.LogInformation(info.ToString());
        //     }
        //     catch (Exception e)
        //     {
        //         response.SetError(OperationMessages.DbError);
        //         info.SetException(e);
        //         _logger.LogError(info.ToString());
        //     }
        //
        //     return response;
        // }
        
        
    }
}
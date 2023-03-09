
namespace SharedLibrary.Helpers
{
    public static class OperationMessages{
        public const string GeneralError = "Hata nedeniyle işleminiz gerçekleştirilemedi";

        public const string DbError = "Veritabanı hatası nedeni ile işlem gerçekleştirilemedi";
        public const string AuthenticateError = "Yanlış kullanıcı adı veya şifre";

        public const string DbItemNotFound = "Kayıt bilgisi bulunamadığından işlem gerçekleştirilemedi";

        public const string NoChanges = "Kayıtta değişiklik yapılmadığı için kayıt edilmedi.";

        public const string Success = "İşleminiz başarıyla gerçekleştirildi";
        public const string BuyMarketItemSuccess = "Satın alma işleminiz başarılı";

        public const string ModelStateNotValid = "Lütfen girilen bilgileri kontrol edip tekrar deneyiniz";

        public const string DuplicateRecord = "Bu kayıt daha önceden eklendiğinden tekrar eklenemez.";
        public const string DuplicateMail = "Bu mail sisteme kayıtlı!";

        public const string TokenFail = "İzinsiz giriş denemesi!";

        public const string UserAllreadyActive = "Kullanıcı zaten aktif olduğundan işleminiz gerçekleşmedi!";
        public const string ProcessAllreadyExist= "İşlem aktif olduğundan işleminiz gerçekleşmedi!";
        public const string HeroAllreadyExist= "Oyuncu heroya zaten sahip!";
        public const string InfoNull = "İşlemi gerçekleştirmek için yeterli bilgiye ulaşılamadı!";
        public const string HeroAllreadyMaxLevel = "Daha fazla yükseltilemez!";
        public const string PlayerHaveNoHero = "Kullanıcı işlem yapmak istediği heroya sahip değil!";
        public const string PlayerHeroBusy = "Kullanıcının işlem yapmak istediği hero müsait değil!";
        public const string PlayerIsUnderProtection = "Saldırı yapmak için her iki tarafın da prison,hospital ve barrack binaları olmalı ";
        public const string PlayerDoesNotHaveResource = "Kullanıcı gerekli kaynaklara sahip değil!";
        public const string TrainingMustBeDone = "Eğitimin bitmesini bekleyin!";
        public const string HealingMustBeDone = "İyileştirme işleminin bitmesini bekleyin!";
        public const string UpgradingMustBeDone = "Bina yükseltme işleminin bitmesini bekleyin!";
        public const string ItemNotUsable = "Bu item kullanılamaz!";
        public const string ItemNotUsableWhileAttack = "Aktif attack veya rally işlemi varken bu item kullanılamaz!";
        public const string MaxLevel = "Tebrikler son seviyedesiniz.";
        public const string InputError = "Beklenen input değerine ulaşılamadı!!";
        public const string PlayerAllreadyGangMember = "Oyuncu zaten bir çete üyesi!!";
        public const string GangAllreadyExist = "Aynı isimli bir çete zaten mevcut!!";
        public const string PlayerIsNotInGangMember = "Oyuncu çete çete üye değil!!";
        public const string PlayerNotHavePermission = "Oyuncu gerekli yetkiye sahip değil!!";
        public const string GangShortNameTaken = "Bu kısaltma başka bir çeteyi temsil ediyor!!";
        public const string ItemBuyedButNotUsed = "Item envantere aktarıldı!";
        public const string BeforeUpgradeThis = "Önce şu binayı yükseltmelisin!";
        public const string BuildingAllreadyExist = "Bina zaten aktif olduğundan işleminiz gerçekleşmedi!";
        public const string WaveRewardAllreadyReceived = "Bu wave ödülleri zaten alınmış!!";
        public const string NoReward = "Ödül yok!";
        public const string WrongCoordinate = "Hatalı koordinat!!";
        public const string GangInviteTimeout = "Davet artık geçersiz!!";
        public const string GangCapacityFull = "Gang kapasitesi dolu!";
        public const string TargetHasCityShield = "Saldırmak istediğiniz kullanıcının kalkanı var. Saldıramazsınız!!";
        public const string CantJoinRally = "Bu rallye artık katılamazsınız!";
        public const string CantJoinGangWithApplication = "Bu gang sadece davet ile üye alıyor!";
        
        
        public const string SengGangApplicationSuccess = "Çete davet gönderme başarılı şekilde gerçekleşti.";
        public const string SengGangApplicationError = "Çete davet gönderme başarısız oldu.";
        
        public const string DestroyGangSuccess = "Çeteyi kapatma başarılı şekilde gerçekleşti.";
        public const string DestroyGangError = "Çete kapatma başarısız oldu.";
        
        public const string KickMemberSuccess = "Üye başarılı şekilde atıldı.";
        public const string KickMemberError = "Üye atma başarısız oldu.";
        
        public const string ChangeMemberTypeSuccess = "Üye rol değiştirme başarılı bir şekilde gerçekleşti.";
        public const string ChangeMemberTypeError = "Üye rol değiştirme başarısız oldu.";
        
        public const string ApplicationAcceptSuccess = "Üye başarılı bir şekilde kabul edildi.";
        public const string ApplicationAcceptError = "Üye kabul etme başarısız oldu.";
        
        public const string SetMemberTypesSuccess = "Çete üye yetkileri başarılı şekilde değiştirildi.";
        public const string SetMemberTypesError = "Çete üye yetkileri değiştirme başarısız oldu.";
        
        public const string GangEditFunctSuccess = "Çete düzenleme başarılı bir şekilde gerçekleştirildi.";
        public const string GangEditFunctError = "Çete düzenleme başarısız oldu.";
        
        public const string ChangeAvatarPlayerBaseSuccess = "Avatar değiştirme başarılı bir şekilde gerçekleştirildi.";
        public const string ChangeAvatarPlayerBaseError = "Avatar değiştirme başarısız oldu.";
        
        public const string ChangeNameSuccess = "İsim değiştirme başarılı bir şekilde gerçekleştirildi.";
        public const string ChangeNameError = "İsim değiştirme başarısız oldu.";
        
        public const string ChangeDiscriptionSuccess = "Açıklama değiştirme başarılı bir şekilde gerçekleştirildi.";
        public const string ChangeDiscriptionError = "Açıklama değiştirme başarısız oldu.";
        
        public const string OnGangCreatedSuccess = "Gang oluşturma başarılı bir şekilde gerçekleştirildi.";
        public const string OnGangCreatedError = "Gang oluşturma başarısız oldu.";
        
        public const string SetAddPlayerBaseBuildingSuccess = "Bina başarılı bir şekilde kuruldu.";
        public const string SetAddPlayerBaseBuildingError = "Bina kurma başarısız oldu.";
        
        public const string SetUpgradeBuildingRequestSuccess = "Bina yükseltme işlemi başarılı bir şekilde başlatıldı.";
        public const string SetUpgradeBuildingRequestError = "Bina yükseltme işlemi başlatılamadı.";
        
        public const string SetUpgradeBuildingDoneRequestSuccess = "Bina yükseltme başarılı bir şekilde yapıldı.";
        public const string SetUpgradeBuildingDoneRequestError = "Bina yükseltme yapılamadı.";
        
        public const string CollectResourcesSuccess = "Kaynaklar başarılı bir şekilde toplandı.";
        public const string CollectResourcesError = "Kaynakları toplama başarısız oldu.";
        
        public const string ExecutePrisonerSuccess = "İdam etme başarılı bir şekilde gerçekleştirildi.";
        public const string ExecutePrisonerError = "İdam etme başarısız oldu.";
        
        public const string PrisonerTrainingReqSuccess = "Mahkum eğitme başarılı bir şekilde başlatıldı.";
        public const string PrisonerTrainingReqError = "Mahkum eğitimi başlatılamadı.";
        
        public const string PrisonerTrainingDoneReqSuccess = "Mahkum eğitme başarılı bir şekilde gerçekleştirildi.";
        public const string PrisonerTrainingDoneReqError = "Mahkum eğitimi yapılamadı.";
        
        public const string HealingRequestSuccess = "Yaralı iyileştirmeye başarılı bir şekilde başlandı.";
        public const string HealingRequestError = "Yaralı iyileştirme başarısız oldu.";
        
        public const string HealingDoneRequestSuccess = "Yaralı iyileştirmeye başarılı bir şekilde yapıldı.";
        public const string HealingDoneRequestError = "Yaralı iyileştirme başarısız oldu.";
        
        public const string BuyMarketDTOSuccess = "Eşya başarılı bir şekilde satın alındı.";
        public const string BuyMarketDTOError = "Eşya satın alma başarısız oldu.";
        
        public const string UseItemFuncSuccess = "Eşya başarılı bir şekilde kullanıldı.";
        public const string UseItemFuncError = "Eşya kullanma başarısız oldu.";
        
        public const string SpeedUpHealingSuccess = "Yaralı iyileştirme başarılı bir şekilde hızlandırıldı.";
        public const string SpeedUpHealingError = "Yaralı iyileştirme hızlandırılamadı.";
        
        public const string SpeedUpTrainingSuccess = "Mahkum eğitimi başarılı bir şekilde hızlandırıldı.";
        public const string SpeedUpTrainingError = "Mahkum eğitimi hızlandırılamadı.";
        
        public const string SpeedUpBuildingUpgradeSuccess = "Bina yükseltme başarılı bir şekilde hızlandırıldı.";
        public const string SpeedUpBuildingUpgradeError = "Bina yükseltme hızlandırılamadı.";
        
        public const string ScoutTrainingRequestSuccess = "Casus eğitimi başarılı bir şekilde başlandı.";
        public const string ScoutTrainingRequestError = "Casus eğitimi başarısız oldu.";
        
        public const string ScoutTrainingDoneRequestSuccess = "Casus eğitimi başarılı bir şekilde yapıldı.";
        public const string ScoutTrainingDoneRequestError = "Casus eğitimi başarısız oldu.";
        
        public const string gangInvitationOnDoneSuccess = "Çete daveti gönderme başarılı.";
        public const string gangInvitationOnDoneError = "Çete daveti gönderme başarısız.";
        
        public const string JoinRallyEventOnDoneSuccess = "Toplu savaşa girme başarılı.";
        public const string JoinRallyEventOnDoneError = "Toplu savaşa girme başarısız.";
        
        public const string SendReinforcementEventOnDoneSuccess = "Destek gönderme başarılı.";
        public const string SendReinforcementEventOnDoneError = "Destek gönderme başarısız.";
        
        public const string AttackEventDTOSuccess = "Saldırı gönderme başarılı.";
        public const string AttackEventDTOError = "Saldırı gönderme başarısız.";
        
        public const string RallyEventOnDoneSuccess = "Toplu saldırı oluşturuldu.";
        public const string RallyEventOnDoneError = "Toplu saldırı oluşturma başarısız oldu.";
        
        public const string AddHeroTalentNodeByNodeIDSuccess = "Hero yeteneği başarılı bir şekilde arttırıldı.";
        public const string AddHeroTalentNodeByNodeIDError = "Hero yeteneği arttırma başarısız oldu.";
        
        public const string UseHeroExpSuccess = "Hero seviyesi başarılı bir şekilde arttırıldı.";
        public const string UseHeroExpError = "Hero seviyesi arttırma başarısız oldu.";
        
        public const string UpgradeHeroSkillSuccess = "Hero yeteneği başarılı bir şekilde arttırıldı.";
        public const string UpgradeHeroSkillError = "Hero yeteneği arttırma başarısız oldu.";
        
        public const string BuyHeroByHeroIdSuccess = "Hero başarılı bir şekilde satın alındı.";
        public const string BuyHeroByHeroIdError = "Hero satın alma başarısız oldu.";
        
        public const string SpeedUpResearchNodeSuccess = "Araştırma başarılı bir şekilde hızlandırıldı.";
        public const string SpeedUpResearchNodeError = "Araştırma hızlandırma başarısız oldu.";
        
        public const string UpgradeResearchNodeDoneSuccess = "Araştırma başarılı bir şekilde yapıldı.";
        public const string UpgradeResearchNodeDoneError = "Araştırma yapılamadı.";
        
        public const string UpgradeResearchNodeSuccess = "Araştırmaya başarılı bir şekilde başlandı.";
        public const string UpgradeResearchNodeError = "Araştırma yapılamadı.";
    
    
    
    
    }
}
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Entities
{
    public class LogAction
    {
        [Key]
        public long Id { get; set; }
        public string? EventId { get; set; }
        public string? EventName { get; set; }
        public string Ip { get; set; }
        public string? DeviceId { get; set; } = String.Empty;
        public long UserId { get; set; } = 0;
        public string? DeviceType { get; set; }
        public string? DeviceModel { get; set; }
        public string? OsVersion { get; set; }
        public string? AppVersion { get; set; }
        public DateTimeOffset? Created { get; set; }
        public double? Duration { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? Action { get; set; }
        public string? Body { get; set; }
    }
}


using Newtonsoft.Json;

namespace SharedLibrary.Models
{
    public class InfoDto
    {
        public string? Ip { get; set; } = String.Empty;
        public long? UserId { get; set; } = 0;
        public string DeviceId { get; set; } = String.Empty;
        public string? DeviceType { get; set; }
        public string? DeviceModel { get; set; }
        public string? OsVersion { get; set; }
        public string? AppVersion { get; set; }

        //public InfoDto(string? deviceId, string? deviceType, string? deviceModel, string? osVersion, string? appVersion)
        //{
        //    DeviceId = deviceId ?? "";
        //    DeviceType = deviceType;
        //    DeviceModel = deviceModel;
        //    OsVersion = osVersion;
        //    AppVersion = appVersion;
        //}


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None
            }).ToString();
        }

        public static InfoDto GetFromString(string value)
        {
            var x = JsonConvert.DeserializeObject<InfoDto>(value);
            return x;
        }
    }
}

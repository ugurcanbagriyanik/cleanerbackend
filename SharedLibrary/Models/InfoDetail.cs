
using Newtonsoft.Json;
using SharedLibrary.Helpers;

namespace SharedLibrary.Models
{
    public class InfoDetail : InfoDto
    {

        public DateTimeOffset? Created { get; set; }
        public string? AdditionalInfo { get; set; }
        public double? Duration { get; set; }
        public string? Action { get; set; }
        public string? Body { get; set; }
        public string Exception { get; set; } = string.Empty;
        public string InnerException { get; set; } =string.Empty;

        //public InfoDetail(string? ip, long? userId, string? deviceId, string? deviceType, string? deviceModel, string? osVersion, string? appVersion, string? additionalInfo) : base(deviceId, deviceType, deviceModel, osVersion, appVersion)
        //{
        //    Ip = ip ?? "";
        //    UserId = userId ?? 0;
        //    Created = DateTimeOffset.Now;
        //    AdditionalInfo = additionalInfo;
        //}

        public InfoDetail()
        {

        }

        public static InfoDetail CreateInfo<T>(BaseRequest<T> req, string action)
        {
            var newDetail = new InfoDetail();

            if (req.Info == null)
            {
                throw new Exception(OperationMessages.InfoNull);
            }
            newDetail.Ip = req.Info.Ip;
            newDetail.Action = action;
            newDetail.AdditionalInfo = string.Empty;
            newDetail.Body = JsonConvert.SerializeObject(req.Data, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None
            }).ToString();
            newDetail.UserId= req.Info.UserId;
            newDetail.Created = DateTimeOffset.Now;
            newDetail.DeviceId = req.Info.DeviceId;
            newDetail.DeviceType = req.Info.DeviceType;
            newDetail.DeviceModel = req.Info.DeviceModel;
            newDetail.OsVersion = req.Info.OsVersion;
            newDetail.AppVersion = req.Info.AppVersion;
            return newDetail;
        }

        public static InfoDetail CreateInfo(BaseRequest req, string action)
        {
            var newDetail = new InfoDetail();

            if (req.Info == null)
            {
                throw new Exception(OperationMessages.InfoNull);
            }
            newDetail.Ip = req.Info.Ip;
            newDetail.Action = action;
            newDetail.AdditionalInfo = string.Empty;
            newDetail.Body = "";
            newDetail.UserId = req.Info.UserId;
            newDetail.Created = DateTimeOffset.Now;
            newDetail.DeviceId = req.Info.DeviceId;
            newDetail.DeviceType = req.Info.DeviceType;
            newDetail.DeviceModel = req.Info.DeviceModel;
            newDetail.OsVersion = req.Info.OsVersion;
            newDetail.AppVersion = req.Info.AppVersion;
            return newDetail;
        }


        public void AddInfo(string exInfo)
        {
            if (this == null)
            {
                return;
            }
            if (AdditionalInfo == null || AdditionalInfo == string.Empty)
            {
                AdditionalInfo = exInfo;
            }
            else
            {
                AdditionalInfo += "\n --" + exInfo;
            }
        }

        public void SetUser(long? userId)
        {
            if (this == null)
            {
                return;
            }
            UserId = userId;
        }

        public void SetException(Exception ex)
        {
            if (this == null)
            {
                return;
            }
            Exception = ex.ToString();
            if (ex.InnerException!=null)
            {
                InnerException = ex.InnerException.ToString();
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }).ToString();
        }

        public static InfoDetail GetFromString(string value)
        {
            var x = JsonConvert.DeserializeObject<InfoDetail>(value);
            return x;
        }
    }
}

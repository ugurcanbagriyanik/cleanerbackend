using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class BaseRequest
    {
        public InfoDto? Info { get; set; } = null;

        public void SetUser(long? userId)
        {
            if (Info == null)
            {
                return;
            }
            Info.UserId = userId;
        }

        public void SetIp(string? ip)
        {
            if (Info == null)
            {
                return;
            }
            Info.Ip = ip;
        }
    }
    public class BaseRequest<T> : BaseRequest
    {
        public T? Data { get; set; } = default;
    }
}

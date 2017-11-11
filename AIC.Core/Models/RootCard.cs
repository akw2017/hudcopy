using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public class RootCard
    {
        public RequestCommand RequestCommand { get; set; }
        public List<ResponseError> ResponseError { get; set; }
        public MainControlCard MainControlCard { get; set; }
        public List<WireMatchingCard> WireMatchingCard { get; set; }
        public WirelessReceiveCard WirelessReceiveCard { get; set; }
    }

    public class RequestCommand
    {
        public int Type { get; set; }
    }

    public class ResponseError
    {
        public string Content { get; set; }
        public int Grade { get; set; }
        public int Type { get; set; }
        public string DateTime { get; set; }

    }

    public enum RequestType
    {
        GetHardWareInfo = 0, //服务获取硬件信息
        ModifiedConfig = 1,//修改硬件配置
        DeleteConfig = 2 //删除硬件配置    
    }

}

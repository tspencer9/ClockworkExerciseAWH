using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clockwork.Web.Models
{
    public class CurrentTimeArray
    {
        [JsonProperty("TimeArray")]
        public CurrentTimeQuery[] Property1 { get; set; }
    }

    public class CurrentTimeQuery
    {
        public int currentTimeQueryId { get; set; }
        public DateTime time { get; set; }
        public string clientIp { get; set; }
        public DateTime utcTime { get; set; }
    }

}
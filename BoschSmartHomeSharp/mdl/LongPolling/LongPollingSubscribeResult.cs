using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.LongPolling
{
    public class LongPollingSubscribeResult
    {
        public string result { get; set; }
        public string jsonrpc { get; set; }

        public static string Serialize(LongPollingSubscribeResult longPolling)
        {
            return JsonConvert.SerializeObject(longPolling);
        }

        public static LongPollingSubscribeResult Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<LongPollingSubscribeResult>(json);
        }
    }
}

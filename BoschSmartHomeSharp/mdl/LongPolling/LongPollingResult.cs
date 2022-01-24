using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.LongPolling
{
    public class LongPollingResult
    {
        public string result { get; set; }
        public string jsonrpc { get; set; }

        public static string Serialize(LongPollingResult longPolling)
        {
            return JsonConvert.SerializeObject(longPolling);
        }

        public static LongPollingResult Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<LongPollingResult>(json);
        }
    }
}

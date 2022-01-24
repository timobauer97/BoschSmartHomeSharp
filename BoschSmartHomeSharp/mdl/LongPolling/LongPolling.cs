using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.LongPolling
{
    public class LongPolling
    {
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public List<string> @params { get; set; }

        public static string Serialize(LongPolling longPolling)
        {
            return JsonConvert.SerializeObject(longPolling);
        }

        public static LongPolling Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<LongPolling>(json);
        }
    }
}

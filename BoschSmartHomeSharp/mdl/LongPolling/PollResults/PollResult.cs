using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.LongPolling.PollResults
{
    public class PollResultWrapper
    {
        public List<PollResult> result { get; set; }
        public string jsonrpc { get; set; }

        public static string Serialize(PollResultWrapper longPollingPollResult)
        {
            return JsonConvert.SerializeObject(longPollingPollResult);
        }

        public static PollResultWrapper Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<PollResultWrapper>(json);
        }
    }

    public class PollResult
    {
        public string path { get; set; }
        public List<string> operations { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string deviceId { get; set; }

        public JObject state { get; set; }
    }
}

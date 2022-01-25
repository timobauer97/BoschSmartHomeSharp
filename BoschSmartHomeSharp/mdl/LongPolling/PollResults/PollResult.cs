using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.LongPolling.PollResults
{
    public interface IPollResult 
    {
    }

    public class PollResult<T> : IPollResult
    {
        public List<Result<T>> result { get; set; }
        public string jsonrpc { get; set; }

        public static string Serialize(PollResult<T> longPollingPollResult)
        {
            return JsonConvert.SerializeObject(longPollingPollResult);
        }

        public static PollResult<T> Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<PollResult<T>>(json);
        }
    }

    public class Result<T>
    {
        public string path { get; set; }
        public List<string> operations { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string deviceId { get; set; }

        public T state { get; set; }
    }
}

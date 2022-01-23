using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.PowerMeter
{
    public class PowerMeter
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string deviceId { get; set; }
        public PowerMeterState.PowerMeterState state { get; set; }
        public string path { get; set; }

        public static string Serialize(PowerMeter state)
        {
            return JsonConvert.SerializeObject(state);
        }

        public static PowerMeter Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<PowerMeter>(json);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.ClimateControl
{
    public class TemperatureLevel
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string deviceId { get; set; }
        public TemperatureLevelState state { get; set; }
        public string path { get; set; }

        public static string Serialize(TemperatureLevel temperatureLevel)
        {
            return JsonConvert.SerializeObject(temperatureLevel);
        }

        public static TemperatureLevel Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<TemperatureLevel>(json);
        }
    }
}

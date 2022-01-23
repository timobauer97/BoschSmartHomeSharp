using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.ClimateControl
{
    public class RoomClimateControlState
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string setpointTemperature { get; set; }

        public static string Serialize(RoomClimateControlState roomClimateControlState)
        {
            return JsonConvert.SerializeObject(roomClimateControlState);
        }

        public static RoomClimateControlState Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<RoomClimateControlState>(json);
        }
    }
}

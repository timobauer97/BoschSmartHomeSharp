using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.ClimateControl
{
    public class RoomClimateControl
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string deviceId { get; set; }
        public RoomClimateControlState state { get; set; }
        public string path { get; set; }

        public static string Serialize(RoomClimateControl roomClimateControl)
        {
            return JsonConvert.SerializeObject(roomClimateControl);
        }

        public static RoomClimateControl Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<RoomClimateControl>(json);
        }
    }
}

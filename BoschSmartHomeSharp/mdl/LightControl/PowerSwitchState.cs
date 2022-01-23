using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.PowerSwitchState
{
    public class PowerSwitchState
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string switchState { get; set; }

        public static string Serialize(PowerSwitchState powerSwitchState)
        {
            return JsonConvert.SerializeObject(powerSwitchState);
        }

        public static PowerSwitchState Serialize(string json)
        {
            return JsonConvert.DeserializeObject<PowerSwitchState>(json);
        }

        public static PowerSwitchState Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<PowerSwitchState>(json);
        }
    }

}

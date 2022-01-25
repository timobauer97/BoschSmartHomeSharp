using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.LongPolling.PollResults
{
    public class PowerSwitchState
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public int automaticPowerOffTime { get; set; }
        public string switchState { get; set; }
    }
}

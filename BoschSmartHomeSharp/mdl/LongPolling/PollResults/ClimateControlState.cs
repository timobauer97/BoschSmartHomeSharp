using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.LongPolling.PollResults
{
    public class ClimateControlState
    {
        public Schedule schedule { get; set; }
        public string operationMode { get; set; }
        public bool summerMode { get; set; }
        public bool low { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
        public double setpointTemperature { get; set; }
        public int setpointTemperatureForLevelEco { get; set; }
        public double setpointTemperatureForLevelComfort { get; set; }
        public bool ventilationMode { get; set; }
        public string roomControlMode { get; set; }
        public bool boostMode { get; set; }
        public bool supportsBoostMode { get; set; }
    }

    public class Schedule
    {
        public List<Profile> profiles { get; set; }
    }

    public class Profile
    {
        public List<SwitchPoint> switchPoints { get; set; }
        public string day { get; set; }
    }

    public class SwitchPoint
    {
        public int startTimeMinutes { get; set; }
        public Value value { get; set; }
    }

    public class Value
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string temperatureLevel { get; set; }
    }
}

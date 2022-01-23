using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.ClimateControl
{
    public class TemperatureLevelState
    {
        public double temperature { get; set; }

        public static string Serialize(TemperatureLevelState temperatureLevelState)
        {
            return JsonConvert.SerializeObject(temperatureLevelState);
        }

        public static TemperatureLevelState Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<TemperatureLevelState>(json);
        }
    }
}

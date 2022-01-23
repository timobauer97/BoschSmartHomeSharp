using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.PowerMeterState
{
    public class PowerMeterState
    {
        public int powerConsumption { get; set; }
        public int energyConsumption { get; set; }

        public static string Serialize(PowerMeterState state)
        {
            return JsonConvert.SerializeObject(state);
        }

        public static PowerMeterState Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<PowerMeterState>(json);
        }
    }
}

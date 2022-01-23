using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.DoorWindowContact
{
    public class ShutterContactState
    {
        public string value { get; set; }

        public static ShutterContactState Serialize(string json)
        {
            return JsonConvert.DeserializeObject<ShutterContactState>(json);
        }

        public static ShutterContactState Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<ShutterContactState>(json);
        }
    }
}

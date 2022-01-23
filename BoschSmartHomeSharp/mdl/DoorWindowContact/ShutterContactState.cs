using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.DoorWindowContact
{
    public class ShutterContactState
    {
        public string value { get; set; }

        public static string Serialize(ShutterContactState shutterContactState)
        {
            return JsonConvert.SerializeObject(shutterContactState);
        }

        public static ShutterContactState Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<ShutterContactState>(json);
        }
    }
}

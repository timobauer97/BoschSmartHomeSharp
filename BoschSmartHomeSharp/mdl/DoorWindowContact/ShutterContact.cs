using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.DoorWindowContact
{
    public class ShutterContact
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string deviceId { get; set; }
        public DoorWindowContact.ShutterContactState state { get; set; }
        public string path { get; set; }

        public static ShutterContact Serialize(string json)
        {
            return JsonConvert.DeserializeObject<ShutterContact>(json);
        }

        public static ShutterContact Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<ShutterContact>(json);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.RegisterDevice
{
    public class RegisterDevice
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string primaryRole { get; set; }
        public string certificate { get; set; }

        public static string Serialize(RegisterDevice root)
        {
            return JsonConvert.SerializeObject(root);
        }

        public static RegisterDevice Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<RegisterDevice>(json);
        }
    }
}

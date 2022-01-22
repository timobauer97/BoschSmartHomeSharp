using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.RegisterDevice
{
    public static class RegisterDevice
    {
        public static string Serialize(Root root)
        {
            return JsonConvert.SerializeObject(root);
        }

        public static Root Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Root>(json);
        }
    }

    public class Root
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string primaryRole { get; set; }
        public string certificate { get; set; }
    }
}

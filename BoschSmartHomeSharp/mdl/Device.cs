using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoschSmartHome.mdl.Device
{
    public class Device
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string rootDeviceId { get; set; }
        public string id { get; set; }
        public List<string> deviceServiceIds { get; set; }
        public string manufacturer { get; set; }
        public string roomId { get; set; }
        public string deviceModel { get; set; }
        public string profile { get; set; }
        public string iconId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string parentDeviceId { get; set; }
        public List<string> childDeviceIds { get; set; }
        public string serial { get; set; }

        public static List<Device> DeserializeList(string json)
        {
            List<Device> devices = new List<Device>();
            JArray array = JsonConvert.DeserializeObject<JArray>(json);

            foreach(var element in array)
            {
                devices.Add(element.ToObject<Device>());
            }

            return devices;
        }
    }
}

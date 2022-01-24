using System;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using BoschSmartHome.mdl.RegisterDevice;
using BoschSmartHome.mdl.Device;

public class BoschSmartHomeSharp
{
 
    public class ApiClient
    {
        public string IPaddress { get; set; }
        internal X509Certificate2 Certificate { get; private set; }

        string url;
        private static string certFile;
        public ApiClient(string ip)
        {
            IPaddress = ip;
            certFile = Path.Join(Directory.GetCurrentDirectory(), "client_pfx.pfx");
            Certificate = new X509Certificate2(certFile, "12345");
            url = "https://" + IPaddress + ":8444/smarthome";



        }

        public ApiClient(string ip, string certFilePath, string certPassword)
        {
            IPaddress = ip;
            Certificate = new X509Certificate2(certFilePath, certPassword);
            url = "https://" + IPaddress + ":8444/smarthome";



        }

        public ApiClient(string ip, string certFilePath)
        {
            IPaddress = ip;
            Certificate = new X509Certificate2(certFilePath);
            url = "https://" + IPaddress + ":8444/smarthome";
        }
    }
}


public class device
{
    public string id { get; set; }
    public string deviceModel { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string roomId { get; set; }
    public string rootDeviceId { get; set; }
    public string manufacturer { get; set; }
    public string[] deviceServiceIds { get; set; }
    public string parentDeviceId { get; set; }
    public string[] childDeviceIds { get; set; }
    public string profile { get; set; }





}

public class client
{
    public string id { get; set; }
    public string clientType { get; set; }
    public string name { get; set; }
    public string primaryRole { get; set; }
    public string[] roles { get; set; }


}

public class roles
{
    public const string ROLE_DEVICES_WRITE = "ROLE_DEVICES_WRITE";
    public const string ROLE_CLIENTS_WRITE = "ROLE_CLIENTS_WRITE";
    public const string ROLE_SCENARIOS_WRITE = "ROLE_SCENARIOS_WRITE";
    public const string ROLE_RESTRICTED_CLIENT = "ROLE_RESTRICTED_CLIENT";
    public const string ROLE_CLOUD_CAMERA_RW = "ROLE_CLOUD_CAMERA_RW";
    public const string ROLE_AUTOMATION_RULES_WRITE = "ROLE_AUTOMATION_RULES_WRITE";
};





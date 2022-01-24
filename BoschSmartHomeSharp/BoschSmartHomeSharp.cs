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
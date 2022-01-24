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

namespace BoschSmartHome
{
 
    public class BoschApiCredentials
    {
        public string IPaddress { get; set; }
        internal X509Certificate2 Certificate { get; private set; }

        private static string certFile;
        public BoschApiCredentials(string ip)
        {
            IPaddress = ip;
            certFile = Path.Join(Directory.GetCurrentDirectory(), "client_pfx.pfx");
            Certificate = new X509Certificate2(certFile, "12345");



        }

        public BoschApiCredentials(string ip, string certFilePath, string certPassword)
        {
            IPaddress = ip;
            Certificate = new X509Certificate2(certFilePath, certPassword);



        }

        public BoschApiCredentials(string ip, string certFilePath)
        {
            IPaddress = ip;
            Certificate = new X509Certificate2(certFilePath);
        }
    }
}
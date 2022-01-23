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
        string url;
        private static string certFile;
        private X509Certificate2 certificate;
        public ApiClient(string ip)
        {
            IPaddress = ip;
            certFile = Path.Join(Directory.GetCurrentDirectory(), "client_pfx.pfx");
            certificate = new X509Certificate2(certFile, "12345");
            url = "https://" + IPaddress + ":8444/smarthome";



        }

        public ApiClient(string ip, string certFilePath, string certPassword)
        {
            IPaddress = ip;
            certificate = new X509Certificate2(certFilePath, certPassword);
            url = "https://" + IPaddress + ":8444/smarthome";



        }

        public ApiClient(string ip, string certFilePath)
        {
            IPaddress = ip;
            certificate = new X509Certificate2(certFilePath);
            url = "https://" + IPaddress + ":8444/smarthome";



        }


        public int getDeviceOnOffState(device mydevice)
        {
            RestSharp.RestClient client;

            if (mydevice.deviceModel == "BSM")
                client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/PowerSwitch/state");
            else
                client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/BinarySwitch/state");


            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");

            IRestResponse response = client.Execute(request);
            //Debug.WriteLine(response.Content);
            Debug.WriteLine(((int)response.StatusCode));

            Debug.WriteLine(response.Content);

            string payloadJson;

            JToken token = JObject.Parse(response.Content);
            if (mydevice.deviceModel == "BSM")
                payloadJson = token["switchState"].ToString();
            else
                payloadJson = token["on"].ToString();

            Debug.WriteLine(payloadJson);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (payloadJson == "True" || payloadJson == "ON")
                    return 1;
                else if (payloadJson == "False" || payloadJson == "OFF")
                    return 0;
                else
                    return -1;
            }
            else
                return -3;
        }


        public int getDeviceLevelState(device mydevice)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/MultiLevelSwitch/state");


            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            request.AddParameter("application/json", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Debug.WriteLine(((int)response.StatusCode));
            Debug.WriteLine(response.Content);


            JToken token = JObject.Parse(response.Content);
            string payloadJson = token["level"].ToString();

            Debug.WriteLine(payloadJson);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                return Int32.Parse(payloadJson);

            }
            else
                return -3;
        }



        public float getShutterLevelState(device mydevice)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/ShutterControl/state");


            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            request.AddParameter("application/json", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Debug.WriteLine(((int)response.StatusCode));
            Debug.WriteLine(response.Content);


            JToken token = JObject.Parse(response.Content);
            string payloadJson = token["level"].ToString();

            Debug.WriteLine(payloadJson);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                return float.Parse(payloadJson);

            }
            else
                return 666;
        }



        public bool switchOnLightDevice(device mydevice)
        {
            RestSharp.RestClient client;

            if (mydevice.deviceModel == "BSM")
                client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/PowerSwitch/state");
            else
                client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/BinarySwitch/state");

            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            if (mydevice.deviceModel == "BSM")
                request.AddParameter("application/json", "{\r\n    \"@type\": \"powerSwitchState\",\r\n    \"switchState\": \"ON\"\r\n}", ParameterType.RequestBody);
            else
                request.AddParameter("application/json", "{\r\n    \"@type\": \"binarySwitchState\",\r\n    \"on\": true\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            //Debug.WriteLine(response.Content);
            Debug.WriteLine(((int)response.StatusCode));

            Debug.WriteLine(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;
            else
                return false;
        }

        public bool switchOffLightDevice(device mydevice)
        {
            RestSharp.RestClient client;

            if (mydevice.deviceModel == "BSM")
                client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/PowerSwitch/state");
            else
                client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/BinarySwitch/state");

            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            if (mydevice.deviceModel == "BSM")
                request.AddParameter("application/json", "{\r\n    \"@type\": \"powerSwitchState\",\r\n    \"switchState\": \"OFF\"\r\n}", ParameterType.RequestBody);
            else
                request.AddParameter("application/json", "{\r\n    \"@type\": \"binarySwitchState\",\r\n    \"on\": false\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            //Debug.WriteLine(response.Content);
            Debug.WriteLine(((int)response.StatusCode));

            Debug.WriteLine(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;
            else
                return false;
        }


        public bool setDeviceLevelState(device mydevice, int level)
        {
            RestSharp.RestClient client;

            client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/MultiLevelSwitch/state");


            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            request.AddParameter("application/json", "{\r\n    \"@type\": \"multiLevelSwitchState\",\r\n    \"level\": " + level.ToString() + "\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            //Debug.WriteLine(response.Content);
            Debug.WriteLine(((int)response.StatusCode));

            Debug.WriteLine(response.Content);



            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {

                return true;

            }
            else
                return false;
        }


        public bool setShutterLevelState(device mydevice, float level)
        {
            RestSharp.RestClient client;

            client = new RestSharp.RestClient(url + "/devices/" + mydevice.id + "/services/ShutterControl/state");


            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            Debug.WriteLine(level.ToString("0.00", new CultureInfo("en-US", false)));
            request.AddParameter("application/json", "{\r\n    \"@type\": \"shutterControlState\",\r\n    \"level\": " + level.ToString("0.00", new CultureInfo("en-US", false)) + "\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            //Debug.WriteLine(response.Content);
            Debug.WriteLine(((int)response.StatusCode));

            Debug.WriteLine(response.Content);



            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {

                return true;

            }
            else
                return false;
        }
            
            
        public string subscribeLongPoll()
        {
            RestSharp.RestClient client;
            client = new RestSharp.RestClient("https://" + IPaddress + ":8444/remote/json-rpc");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            request.AddParameter("application/json", "[\n    {\n        \"jsonrpc\":\"2.0\",\n        \"method\":\"RE/subscribe\",\n        \"params\": [\"com/bosch/sh/remote/*\", null]\n    }\n]", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            string jsonResponse = "{\"answer\":" + response.Content + "}";
            JToken token = JObject.Parse(jsonResponse);
            string pollId = token["answer"][0]["result"].ToString();
            Debug.WriteLine("sub pollId: " + pollId);
            return pollId;
        }

        public bool unsubscribeLongPoll(string pollId)
        {
            try
            {
                RestSharp.RestClient client;
                client = new RestSharp.RestClient("https://" + IPaddress + ":8444/remote/json-rpc");
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.ClientCertificates = new X509CertificateCollection() { certificate };
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("api-version", "2.1");
                request.AddParameter("application/json", "[\n    {\n        \"jsonrpc\":\"2.0\",\n        \"method\":\"RE/unsubscribe\",\n        \"params\": [\"" + pollId + "\"]\n    }\n]", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                string jsonResponse2 = "{\"answer\":" + response.Content + "}";
                JToken token = JObject.Parse(jsonResponse2);
                var payloadJson = token["answer"][0]["result"].ToString();
                Debug.WriteLine("unsubscribed. " + payloadJson);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }


        public IEnumerable<JToken> longPoll(string pollId)
        {
            RestSharp.RestClient client;
            client = new RestSharp.RestClient("https://" + IPaddress + ":8444/remote/json-rpc");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            request.AddParameter("application/json", "[\n    {\n        \"jsonrpc\":\"2.0\",\n        \"method\":\"RE/longPoll\",\n        \"params\": [\"" + pollId + "\", 30]\n    }\n]", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var JArray = Newtonsoft.Json.Linq.JArray.Parse(response.Content);
            return JArray.Children();
            
        }

        public List<client> getClients()
        {
            RestSharp.RestClient client = new RestSharp.RestClient(url + "/clients");
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");
            IRestResponse response = client.Execute(request);
            string jsonResponse2 = "{\"clients\":" + response.Content + "}";
            JToken token = JObject.Parse(jsonResponse2);
            var payloadJson = token["clients"].ToString();
            return JsonConvert.DeserializeObject<List<client>>(payloadJson);
        }

        /// <summary>
        ///     Fetches the device list from the Bosch Smarthome Controller.<br />
        ///     the Certificate must be paired with the Controller (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <returns>
        ///     <b>List with <see cref="Device"/></b>: Contains all devices, paired with the Smarthome Controller.<br />
        ///     <b>null</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public List<Device> getDevices()
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + IPaddress + ":8444/smarthome/devices")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { certificate },
                Timeout = -1
            };

            var request = new RestRequest(Method.GET);

            //Request Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //TODO Exception Handling
                List<Device> devices = Device.DeserializeList(response.Content);
                Debug.WriteLine($"found {devices.Count} devices.");

                return devices;
            }
            else
            {
                Debug.WriteLine($"Could not fetch devices. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     Registers a client in the Bosch Smarthome Controller. <br />
        ///     <b>NOTE: Before this method is called, you need to press the pairing-button until the LEDs start flashing.</b>
        /// </summary>
        /// <param name="devicePwdBase64">the base64 encoded password of the controller.</param>
        /// <param name="cert">
        ///     the content of the certificate (begins with -----BEGIN CERTIFICATE-----).<br />
        ///     <b>NOTE: As <a href="https://github.com/BoschSmartHome/bosch-shc-api-docs/tree/master/postman#customize-the-certificate">mentioned in the official GitHub repo</a>, the certificate might be modified!</b>
        /// </param>
        /// <param name="clientId">the id of the client</param>
        /// <param name="clientName">the name of the client</param>
        /// <returns>
        ///     <see cref="bool"/><br /> 
        ///     <b>true:</b> the client has been successfully registered.<br />
        ///     <b>false:</b> an error occured.
        /// </returns>
        public bool registerDevice(string devicePwdBase64, string cert, string clientId, string clientName)
        {
            RestClient client = new RestClient("https://" + IPaddress + ":8443/smarthome/clients")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { certificate },
                Timeout = -1
            };

            RestRequest request = new RestRequest(Method.POST);

            // Request Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Systempassword", devicePwdBase64);

            // Request Body
            request.AddParameter("application/json",
                RegisterDevice.Serialize(
                    new RegisterDevice { Type = "client", id = $"oss_{clientId}", name = $"OSS {clientName}", primaryRole = "ROLE_RESTRICTED_CLIENT", certificate = cert }),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Debug.WriteLine("client registration was successfull");
                return true;
            }
            else
            {
                Debug.WriteLine($"client registration failed with Statuscode {response.StatusCode} ({response.StatusDescription}) and content {response.Content}. Exception: {response.ErrorException}");
                return false;
            }


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





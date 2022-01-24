using BoschSmartHome.mdl.Device;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BoschSmartHome.Devices
{
    public static class Devices
    {
        /// <summary>
        ///     Fetches the device list from the Bosch Smarthome Controller.<br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerClient(string, string, string, string)"/>)
        /// </summary>
        /// <returns>
        ///     <b>List with <see cref="Device"/></b>: Contains all devices, paired with the Smarthome Controller.<br />
        ///     <b>null</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public static List<Device> getDevices(BoschApiCredentials credentials)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + ":8444/smarthome/devices")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { credentials.Certificate },
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
    }
}

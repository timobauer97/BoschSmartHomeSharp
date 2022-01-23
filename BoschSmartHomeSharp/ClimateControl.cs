using BoschSmartHome.mdl.ClimateControl;
using BoschSmartHome.mdl.Device;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static BoschSmartHomeSharp;

namespace BoschSmartHome.ClimateControl
{
    public static class ClimateControl
    {
        /// <summary>
        ///     retrives the room climate control data from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the device</param>
        /// <returns>
        ///     <see cref="RoomClimateControl"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static RoomClimateControl getRoomClimateControl(ApiClient credentials, Device device)
        {
            return getRoomClimateControl(credentials, device?.id);
        }

        /// <summary>
        ///     retrives the room climate control data from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="RoomClimateControl"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static RoomClimateControl getRoomClimateControl(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/RoomClimateControl")
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
                RoomClimateControl powerMeter = RoomClimateControl.Deserialize(response.Content);
                Debug.WriteLine($"found PowerMeter {deviceId} devices.");

                return powerMeter;
            }
            else
            {
                Debug.WriteLine($"Could not fetch PowerMeter. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }
    }
}

using BoschSmartHome.mdl.Device;
using BoschSmartHome.mdl.ShutterContactState;
using BoschSmartHome.mdl.ShutterContact;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static BoschSmartHomeSharp;

namespace BoschSmartHome.DoorWindowContact
{
    public static class DoorWindowContact
    {

        /// <summary>
        ///     retrives the shutter contact state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the device</param>
        /// <returns>
        ///     <see cref="ShutterContactState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static ShutterContactState getShutterContactState(ApiClient credentials, Device device)
        {
            return getShutterContactState(credentials, device?.id);
        }

        /// <summary>
        ///     retrives the shutter contact state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="ShutterContactState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static ShutterContactState getShutterContactState(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/ShutterContact/state")
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
                ShutterContactState shutterContactState = ShutterContactState.Deserialize(response.Content);
                Debug.WriteLine($"found ShutterContact {deviceId}. state: {shutterContactState.value}");

                return shutterContactState;
            }
            else
            {
                Debug.WriteLine($"Could not fetch ShutterContactState. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     retrives informations for the shutter contact from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the device</param>
        /// <returns>
        ///     <see cref="ShutterContactState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static ShutterContact getShutterContact(ApiClient credentials, Device device)
        {
            return getShutterContact(credentials, device?.id);
        }

        /// <summary>
        ///     retrives informations for the shutter contact from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="ShutterContactState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static ShutterContact getShutterContact(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/ShutterContact")
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
                ShutterContact shutterContact = ShutterContact.Deserialize(response.Content);
                Debug.WriteLine($"found ShutterContact {deviceId}.");

                return shutterContact;
            }
            else
            {
                Debug.WriteLine($"Could not fetch ShutterContact. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }
    }
}

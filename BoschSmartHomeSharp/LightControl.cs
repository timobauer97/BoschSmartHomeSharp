using BoschSmartHome.mdl.Device;
using BoschSmartHome.mdl.LightControl;
using RestSharp;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

using static BoschSmartHomeSharp;

namespace BoschSmartHome.LightControl
{
    /// <summary>
    /// Contains functions for controlling and monitoring Bosch Smart Home Lightswitches and Powermeters.<br />
    /// For further informations read <a href="https://apidocs.bosch-smarthome.com/local/#/">the official documentation</a> -> Definition Light Control
    /// </summary>
    public static class LightControl
    {
        /// <summary>
        ///     retrives the power meter data from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="device">the device</param>
        /// <returns>
        ///     <see cref="PowerMeter"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static PowerMeter getPowerMeter(ApiClient credentials, Device device)
        {
            return getPowerMeter(credentials, device?.id);
        }

        /// <summary>
        ///     retrives the power meter data from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="PowerMeter"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static PowerMeter getPowerMeter(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/PowerMeter")
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
                PowerMeter powerMeter = PowerMeter.Deserialize(response.Content);
                Debug.WriteLine($"found PowerMeter {deviceId} devices.");

                return powerMeter;
            }
            else
            {
                Debug.WriteLine($"Could not fetch PowerMeter. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     retrives the power switch state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="device">the device</param>
        /// <returns>
        ///     <see cref="PowerSwitchState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static PowerSwitchState getPowerSwitchState(ApiClient credentials, Device device)
        {
            return getPowerSwitchState(credentials, device?.id);
        }

        /// <summary>
        ///     retrives the power switch state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="PowerSwitchState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static PowerSwitchState getPowerSwitchState(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/PowerSwitch/state")
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
                PowerSwitchState powerSwitchState = PowerSwitchState.Deserialize(response.Content);
                Debug.WriteLine($"found PowerSwitch {deviceId}. state: {powerSwitchState.switchState}");

                return powerSwitchState;
            }
            else
            {
                Debug.WriteLine($"Could not fetch PowerSwitchState. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     Sets the state of a powerswitch. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="device">the device.</param>
        /// <param name="state">
        ///     <see cref="bool"/> <br />
        ///     <b>true</b>: the switch will be turned on <br />
        ///     <b>false</b>: the switch will be turned off
        /// </param>
        /// <returns>
        ///     <see cref="bool"/> <br />
        ///     <b>true</b>: the state was successfully set <br />
        ///     <b>false</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public static bool setPowerSwitchState(ApiClient credentials, Device device, bool state)
        {
            return setPowerSwitchState(credentials, device?.id, state);
        }

        /// <summary>
        ///     Sets the state of a powerswitch. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device.</param>
        /// <param name="state">
        ///     <see cref="bool"/> <br />
        ///     <b>true</b>: the switch will be turned on <br />
        ///     <b>false</b>: the switch will be turned off
        /// </param>
        /// <returns>
        ///     <see cref="bool"/> <br />
        ///     <b>true</b>: the state was successfully set <br />
        ///     <b>false</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public static bool setPowerSwitchState(ApiClient credentials, string deviceId, bool state)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/PowerSwitch/state")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { credentials.Certificate },
                Timeout = -1
            };

            var request = new RestRequest(Method.PUT);

            //Request Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");

            //Request Body
            request.AddParameter("application/json",
                PowerSwitchState.Serialize(
                    new PowerSwitchState { Type = "powerSwitchState", switchState = state ? "ON" : "OFF" }),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            // No-Content is OK
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Debug.WriteLine($"set state of device {deviceId} to {(state ? "ON" : "OFF")}");
                return true;
            }
            else
            {
                Debug.WriteLine($"Could not set power switch state. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return false;
            }
        }
    }
}

using BoschSmartHome.mdl.ClimateControl;
using BoschSmartHome.mdl.Device;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        /// <param name="device">the device</param>
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
                RoomClimateControl roomClimateControl = RoomClimateControl.Deserialize(response.Content);
                Debug.WriteLine($"found RoomClimateControl {deviceId}.");

                return roomClimateControl;
            }
            else
            {
                Debug.WriteLine($"Could not fetch RoomClimateControl. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     retrives the temperature level from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="device">the device</param>
        /// <returns>
        ///     <see cref="TemperatureLevel"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static TemperatureLevel getTemperatureLevel(ApiClient credentials, Device device)
        {
            return getTemperatureLevel(credentials, device?.id);
        }

        /// <summary>
        ///     retrives the temperature level from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="TemperatureLevel"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static TemperatureLevel getTemperatureLevel(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/TemperatureLevel")
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
                TemperatureLevel temperatureLevel = TemperatureLevel.Deserialize(response.Content);
                Debug.WriteLine($"found TemperatureLevel {deviceId}.");

                return temperatureLevel;
            }
            else
            {
                Debug.WriteLine($"Could not fetch TemperatureLevel. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     retrives the room climate control state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="device">the device</param>
        /// <returns>
        ///     <see cref="RoomClimateControlState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static RoomClimateControlState getRoomClimateControlState(ApiClient credentials, Device device)
        {
            return getRoomClimateControlState(credentials, device?.id);
        }

        /// <summary>
        ///     retrives the room climate control state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="RoomClimateControlState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static RoomClimateControlState getRoomClimateControlState(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/RoomClimateControl/state")
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
                RoomClimateControlState roomClimateControlState = RoomClimateControlState.Deserialize(response.Content);
                Debug.WriteLine($"found RoomClimateControlState {deviceId}.");

                return roomClimateControlState;
            }
            else
            {
                Debug.WriteLine($"Could not fetch RoomClimateControlState. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     retrives the temperature level state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="device">the device</param>
        /// <returns>
        ///     <see cref="TemperatureLevelState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static TemperatureLevelState getTemperatureLevelState(ApiClient credentials, Device device)
        {
            return getTemperatureLevelState(credentials, device?.id);
        }

        /// <summary>
        ///     retrives the temperature level state from the Smarthome Controller. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device</param>
        /// <returns>
        ///     <see cref="TemperatureLevelState"/><br />
        ///     <b>value</b>: the received data
        ///     <b>null</b>: the request failed. See Debug-log for more informations.
        /// </returns>
        public static TemperatureLevelState getTemperatureLevelState(ApiClient credentials, string deviceId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/TemperatureLevel/state")
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
                TemperatureLevelState temperatureLevelState = TemperatureLevelState.Deserialize(response.Content);
                Debug.WriteLine($"found TemperatureLevelState {deviceId}.");

                return temperatureLevelState;
            }
            else
            {
                Debug.WriteLine($"Could not fetch TemperatureLevelState. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     Sets the temperature state of a Bosch Smarthome thermostate. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="device">the device.</param>
        /// <param name="setpointTemperature">
        ///     <see cref="double"/> <br />
        ///     <b>value</b>: the setpoint temperature
        /// </param>
        /// <returns>
        ///     <see cref="bool"/> <br />
        ///     <b>true</b>: the state was successfully set <br />
        ///     <b>false</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public static bool setRoomClimateControlState(ApiClient credentials, Device device, double setpointTemperature)
        {
            return setRoomClimateControlState(credentials, device?.id, setpointTemperature);
        }

        /// <summary>
        ///     Sets the temperature state of a Bosch Smarthome thermostate. <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="deviceId">the id of the device.</param>
        /// <param name="setpointTemperature">
        ///     <see cref="double"/> <br />
        ///     <b>value</b>: the setpoint temperature
        /// </param>
        /// <returns>
        ///     <see cref="bool"/> <br />
        ///     <b>true</b>: the state was successfully set <br />
        ///     <b>false</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public static bool setRoomClimateControlState(ApiClient credentials, string deviceId, double setpointTemperature)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/smarthome/devices/{deviceId}/services/RoomClimateControl/state")
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
                RoomClimateControlState.Serialize(
                    new RoomClimateControlState { Type = "climateControlState", setpointTemperature = setpointTemperature.ToString(CultureInfo.InvariantCulture) }),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            // No-Content is OK
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Debug.WriteLine($"set temperature state of device {deviceId} to {setpointTemperature}");
                return true;
            }
            else
            {
                Debug.WriteLine($"Could not set temperature state. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return false;
            }
        }
    }
}

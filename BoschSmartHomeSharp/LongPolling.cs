using BoschSmartHome.mdl.LongPolling;
using BoschSmartHome.mdl.LongPolling.PollResults;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using LongPollingModel = BoschSmartHome.mdl.LongPolling.LongPolling;

namespace BoschSmartHome.LongPolling
{
    /// <summary>
    /// Contains functions for controlling and monitoring Bosch Smart Home LongPolling.<br />
    /// For further informations read <a href="https://github.com/BoschSmartHome/bosch-shc-api-docs/tree/master/best_practice#use-long-polling-instead-of-short-polling">the official documentation</a>
    /// </summary>
    public static class LongPolling
    {
        /// <summary>
        ///     Subscribes to the Bosch SmartHome HTTP long polling. The poll id which is returned by this function can be used for poll requests.<br />
        ///     see <see cref="poll(BoschApiCredentials, string)"/> <br />
        ///     <b>NOTE</b>: As mentioned in the <a href="https://github.com/BoschSmartHome/bosch-shc-api-docs/tree/master/best_practice#use-long-polling-instead-of-short-polling">the official documentation</a>, the pollId expires after 24h!<br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <returns>
        ///     <see cref="string"/> <br />
        ///     <b>value</b>: the poll id which can be used for poll requests <br />
        ///     <b>null</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public static string subscribe(BoschApiCredentials credentials)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/remote/json-rpc")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { credentials.Certificate },
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);

            //Request Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");

            //Request Body
            request.AddParameter("application/json",
                LongPollingModel.Serialize(
                    new LongPollingModel { jsonrpc = "2.0", method = "RE/subscribe", @params = new List<string> { "com/bosch/sh/remote/*", null } }),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LongPollingSubscribeResult longPollingResult = LongPollingSubscribeResult.Deserialize(response.Content);

                Debug.WriteLine($"successfully subscribed to longpolling. id: {longPollingResult.result}");
                return longPollingResult.result;
            }
            else
            {
                Debug.WriteLine($"Could not subscribe to longpolling. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     Starts a HTTP long poll request. The request is usually kept open until an event occurs (e.g. Thermostate temperature changed, switch state changed). However if no event occurs for five minutes, the controller ends the request with an timeout.  <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="pollId">the pollId which is assigned at subscription</param>
        /// <returns>
        ///     <see cref="PollResult"/> <br />
        ///     <b>value</b>: the event from the Smarthome controller<br />
        ///     <b>null</b>: The request was empty or failed. (Might be an timeout). See Debug-log for more informations.
        /// </returns>
        public static PollResult poll(BoschApiCredentials credentials, string pollId)
        {
            // TODO: TEST TIMEOUT
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/remote/json-rpc")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { credentials.Certificate },
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);

            //Request Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");

            //Request Body
            request.AddParameter("application/json",
                LongPollingModel.Serialize(
                    new LongPollingModel { jsonrpc = "2.0", method = "RE/longPoll", @params = new List<string> { pollId, "30" } }),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                PollResultWrapper result = PollResultWrapper.Deserialize(response.Content);

                if(result?.result == null || result.result.Count != 1)
                {
                    Debug.WriteLine($"result is {(result?.result == null ? "null." : $"not null. count: {result.result.Count}")}. ");
                    return null;
                }

                //TODO Add Structures/Support for other devices (e.g. DoorWindowContact)
                Debug.WriteLine($"received longpoll. id: {result.result[0].id} deviceId: {result.result[0].deviceId} path: {result.result[0].path}");

                return result.result[0];
            }
            else
            {
                Debug.WriteLine($"Did not receive longpolling answer. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        /// <summary>
        ///     unsubscribes the longpolling.  <br />
        ///     the Certificate must be paired with the Controller. Otherwise the operation will fail (see <see cref="registerDevice(string, string, string, string)"/>)
        /// </summary>
        /// <param name="credentials">the credentials for the communication with the Smarthome Controller</param>
        /// <param name="pollId">the pollId which is assigned at subscription</param>
        /// <returns>
        ///     <see cref="bool"/> <br />
        ///     <b>true</b>: successfully unsubscribed<br />
        ///     <b>false</b>: The request failed. See Debug-log for more informations.
        /// </returns>
        public static bool unsubscribe(BoschApiCredentials credentials, string pollId)
        {
            // TODO Refactor.. NOTE: Different Ports for /smarthome/clients, /smarthome/ /remote/json-rpc, /public/ ...
            RestClient client = new RestClient("https://" + credentials.IPaddress + $":8444/remote/json-rpc")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { credentials.Certificate },
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);

            //Request Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-version", "2.1");

            //Request Body
            request.AddParameter("application/json",
                LongPollingModel.Serialize(
                    new LongPollingModel { jsonrpc = "2.0", method = "RE/unsubscribe", @params = new List<string> { pollId } }),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.WriteLine($"successfully unsubscribed to longpolling. id: {pollId}");
                return true;
            }
            else
            {
                Debug.WriteLine($"Could not unsubscribe longpolling. id: {pollId} Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return false;
            }
        }
    }
}

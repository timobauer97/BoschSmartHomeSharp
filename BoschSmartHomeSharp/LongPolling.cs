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
    public static class LongPolling
    {

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

        public static PollResult poll(BoschApiCredentials credentials, string pollId)
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
                Debug.WriteLine($"Could not subscribe to longpolling. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

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

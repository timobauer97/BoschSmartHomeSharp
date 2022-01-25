using BoschSmartHome.mdl.LongPolling;
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
                LongPollingResult longPollingResult = LongPollingResult.Deserialize(response.Content);

                Debug.WriteLine($"successfully subscribed to longpolling. id: {longPollingResult.result}");
                return longPollingResult.result;
            }
            else
            {
                Debug.WriteLine($"Could not subscribe to longpolling. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        public static string poll(BoschApiCredentials credentials, string pollId)
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

                Debug.WriteLine($"received longpoll");
                throw new NotImplementedException();
            }
            else
            {
                Debug.WriteLine($"Could not subscribe to longpolling. Statuscode: {response.StatusCode} ({response.StatusDescription}) content: {response.Content}. Exception: {response.ErrorException}");

                return null;
            }
        }

        public static bool unsubscribe(BoschApiCredentials credentials, string pollId)
        {
            throw new NotImplementedException();
        }
    }
}

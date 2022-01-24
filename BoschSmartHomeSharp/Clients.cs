using BoschSmartHome.mdl.RegisterDevice;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BoschSmartHome.Clients
{
    public static class Clients
    {
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
        ///     <b>false:</b> an error occured. See Debug-log for more informations.
        /// </returns>
        public static bool registerClient(BoschApiCredentials credentials, string devicePwdBase64, string cert, string clientId, string clientName)
        {
            RestClient client = new RestClient("https://" + credentials.IPaddress + ":8443/smarthome/clients")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ClientCertificates = new X509CertificateCollection() { credentials.Certificate },
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

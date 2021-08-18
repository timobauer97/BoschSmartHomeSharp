# BoschSmartHomeSharp
C# wrapper class for the Bosch SmartHome local REST API (https://github.com/BoschSmartHome/bosch-shc-api-docs)

(work in progress)

requires self-signed client certificate to talk with the Bosch smart home controller (by default, the library expects the PFX to be provided as file "client_pfx.PFX", which should be present in the executable directory) and protected with a password (the default library expects '12345' so you'll need to adjust and recompile this libraries source code when your pfx uses a different filename and/or more secure password). The certificate as string, and the password of your smart home controller encoded as base64, are both needed in order to register to the controller as new client app (and to create trust to your client cert).

```C#
BoschSmartHomeSharp.ApiClient apiclient;
(...)
apiclient = new BoschSmartHomeSharp.ApiClient("192.168.0.10");
(...)
private void registerClient // register new client
        {
            string clientCert = "\"-----BEGIN CERTIFICATE-----\\rMIID(...)A==\\r-----END CERTIFICATE-----\"";
            bool result = apiclient.registerDevice("c29tZXRoaW5nMTIzIQ==", clientCert, "myNewBoschClientApp");
            if (!result)
                Debug.WriteLine("registration not successful");
            else
                Debug.WriteLine("registration successful - yay!! :)");
        }



```


```C#
List<device> allDevices;
(...)
allDevices = apiclient.getDevices();
```

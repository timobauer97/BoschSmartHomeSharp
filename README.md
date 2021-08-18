# BoschSmartHomeSharp
C# wrapper class for Bosch SmartHome REST API (work in progress)

```C#
BoschSmartHomeSharp.ApiClient apiclient;
allDevices = new List<device>();
apiclient = new BoschSmartHomeSharp.ApiClient("192.168.0.10");
allDevices = apiclient.getDevices();
```

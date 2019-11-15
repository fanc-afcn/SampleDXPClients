
# SampleDXPClients
Sample Clients to interact with the FANC Data eXchange Platform API.
**Remark:** This code is only usable by approved FANC partners who obtained credentials to connect to the FANC DXP API.

The projects make use of various FANC NuGet packages. The consuming application should target at least .NET 4.6.1 and preferably .NET 4.7.2 or higher (or .NET Core 2 or higher)

### SamplePHIClient

Contains sample code for FANC approved partners of the Physical Inventory module (PHI).
You'll need to set the adequate appSettings (obtained from FANC) in the App.config file.
Targets .NET 4.7.2.

### SampleJSClient

Contains sample code on how to interact with the new FANC API v2 via the means of a JavaScript client.
**Important:** make sure you run the project locally from url https://localhost:5001
**The v2 API will replace the v1 API in the future. For internal usage only for the time being**

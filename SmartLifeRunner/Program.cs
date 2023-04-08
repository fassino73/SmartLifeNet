using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using SmartLifeNet;
using SmartLifeRunner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();
app.Urls.Add("https://*:5000");

var email = builder.Configuration["SmartLife:email"];
var password = builder.Configuration["SmartLife:password"];

if (string.IsNullOrEmpty(email))
{
    throw new Exception("Missing SmartLife email from config");
}

if (string.IsNullOrEmpty(password))
{
    throw new Exception("Missing SmartLife password from config");
}

var deviceManager = new DeviceManager(email, password);

await deviceManager.InitializeDevicesAsync();

app.MapGet("/", () => "It works!");

// example:
//      localhost:5000/api/plug/ab60123458093435eeeejk/state

app.MapGet("/api/plug/{deviceId}/state", async (string deviceId) =>
{
    var device = deviceManager.GetSwitch(deviceId);

    if (device == null)
    {
        return Results.NotFound();
    }

    var state = await device.GetState();

    return Results.Ok(device.data);
});

app.MapPost("/api/plug/{deviceId}/state", async (string deviceId, [FromBody] SmartPlugRequest request) =>
{
    var device = deviceManager.GetSwitch(deviceId);

    if (device == null)
    {
        return Results.NotFound();
    }

    if (request.State == SmartPlugState.On)
    {
        var response = await device.TurnOn();
        return Results.Ok(response);
    }
    else
    {
        var response = await device.TurnOff();
        return Results.Ok(response);
    }
});

app.Run();
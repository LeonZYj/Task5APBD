using Device.BusinessLogic.Services;
using DeviceManager.Entries.Devices;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ABCConnection");

builder.Services.AddTransient<IServiceMethods, ServiceMethod>(
    _ => new ServiceMethod(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/api/devices", (IServiceMethods serviceMethod) =>
    {
        try
        {
            return Results.Ok(serviceMethod.getAllDevices());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    })
    .WithName("GetAllDevices")
    .WithOpenApi();

app.MapPost("/api/devices", (IServiceMethods serviceMethod, DeviceNonAbstractClass device) =>
{
    try
    {
        var result = serviceMethod.addDevice(device);
        if (result is true)
        {
            return Results.Created();
        }
        else
        {


            return Results.BadRequest();

        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapDelete("/api/devices/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.deleteDevice(id);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.NotFound();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/devices/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.getDeviceID(id);
        if (result is not null)
        {
            return Results.Ok(result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("api/devices/personalComputers", (IServiceMethods serviceMethod) =>
{
    try
    {
        {
            return Results.Ok(serviceMethod.getAllPersonalComputers());
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("api/devices/smartWatches", (IServiceMethods serviceMethod) =>
{
    try
    {
        {
            return Results.Ok(serviceMethod.getAllSmartWatches());
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("api/devices/embeddedDevices", (IServiceMethods serviceMethod) =>
{
    try
    {
        {
            return Results.Ok(serviceMethod.getAllEmbeddedDevices());
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPut("/api/devices/{id}", (IServiceMethods serviceMethod, string id, DeviceNonAbstractClass device) =>
{
    try
    {
        var result = serviceMethod.updateDevice(device);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("/api/devices/personalComputers/{id}", (IServiceMethods serviceMethod, string id, PersonalComputer device) =>
{
    try
    {
        var result = serviceMethod.updatePersonalComputer(device);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("/api/devices/smartWatches/{id}", (IServiceMethods serviceMethod, string id, SmartWatch device) =>
{
    try
    {
        var result = serviceMethod.updateSmartWatch(device);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("/api/devices/embeddedDevices/{id}", (IServiceMethods serviceMethod, string id, EmbeddedDevice device) =>
{
    try
    {
        var result = serviceMethod.updateEmbeddedDevice(device);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/devices/personalComputers/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.getPersonalComputerID(id);
        if (result is not null)
        {
            return Results.Ok(result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/devices/smartWatches/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.getSmartWatchID(id);
        if (result is not null)
        {
            return Results.Ok(result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/devices/embeddedDevices/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.getEmbeddedDeviceID(id);
        if (result is not null)
        {
            return Results.Ok(result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/devices/personalComputers", (IServiceMethods serviceMethod, PersonalComputer pc) =>
{
    try
    {
        var result = serviceMethod.addPersonalComputer(pc);
        if (result is true)
        {
            return Results.Created();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/devices/smartWatches", (IServiceMethods serviceMethod, SmartWatch watch) =>
{
    try
    {
        var result = serviceMethod.addSmartWatch(watch);
        if (result is true)
        {
            return Results.Created();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/devices/embeddedDevices", (IServiceMethods serviceMethod, EmbeddedDevice device) =>
{
    try
    {
        var result = serviceMethod.addEmbeddedDevice(device);
        if (result is true)
        {
            return Results.Created();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapDelete("api/devices/personalComputers/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.deletePersonalComputer(id);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.NotFound();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapDelete("api/devices/smartWatches/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.deleteSmartWatch(id);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.NotFound();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapDelete("api/devices/embeddedDevices/{id}", (IServiceMethods serviceMethod, string id) =>
{
    try
    {
        var result = serviceMethod.deleteEmbeddedDevice(id);
        if (result is true)
        {
            return Results.Ok();
        }
        else
        {
            return Results.NotFound();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
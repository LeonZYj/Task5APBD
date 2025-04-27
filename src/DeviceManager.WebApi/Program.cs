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


app.Run();
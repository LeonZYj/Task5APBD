using Device.BusinessLogic.Services;

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

app.Run();
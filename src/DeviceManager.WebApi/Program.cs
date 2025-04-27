using Device.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ABCConnection");

builder.Services.AddTransient<IServiceMethods, ServiceMethod>(
    _=> new ServiceMethod(connectionString));
builder.Configuration.GetConnectionString("ABCConnection");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
});

app.UseHttpsRedirection();



using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for console logging (Seq can be added later)
builder.Host.UseSerilog(( context, config ) =>
{
    config.WriteTo.Console()
        .MinimumLevel.Debug();
});

// Add Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
});

// Add Prometheus metrics
builder.Services.AddMetrics();

var app = builder.Build();

// Middleware pipeline
app.UseSerilogRequestLogging();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
    c.RoutePrefix = string.Empty; // Swagger at root
});

app.UseHttpMetrics(); // Prometheus metrics

app.Logger.LogInformation("Configuring Ocelot middleware...");
await app.UseOcelot();

app.Logger.LogInformation("API Gateway starting on http://localhost:5000");
app.Run();
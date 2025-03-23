using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog(( ctx, lc ) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://seq:5341"));

// Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

// Pipeline
app.UseSerilogRequestLogging();
app.UseMetricServer();
app.UseHttpMetrics();
app.UseOcelot().Wait();

app.Run();
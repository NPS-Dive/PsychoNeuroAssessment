using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PNA.Core.Interfaces;
using Prometheus;
using PNA.Core.Interfaces;
using PNA.TestService.Application.Behaviors;
using PNA.TestService.Infrastructure.Data;
using PNA.TestService.Infrastructure.Services;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog(( ctx, lc ) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://seq:5341"));

// Services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" }));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<ITestResultRepository, MongoTestResultRepository>();
builder.Services.AddScoped<ITestCalculator, TestCalculator>();
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq(( context, cfg ) => cfg.Host(builder.Configuration["RabbitMQ:Host"]));
});

var app = builder.Build();

// Pipeline
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API v1"));
app.UseRouting();
app.UseMetricServer();
app.UseHttpMetrics();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Prometheus;
using PNA.Core.Interfaces;
using PNA.AuthService.Application.Behaviors;
using PNA.AuthService.Infrastructure.Data;
using PNA.AuthService.Infrastructure.Services;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Logging with Serilog and Seq
builder.Host.UseSerilog(( ctx, lc ) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://seq:5341"));

// Services
builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" }));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq(( context, cfg ) => cfg.Host(builder.Configuration["RabbitMQ:Host"]));
});

var app = builder.Build();

// Middleware Pipeline
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API v1"));
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMetricServer(); // Prometheus metrics endpoint
app.UseHttpMetrics(); // Collects HTTP metrics
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
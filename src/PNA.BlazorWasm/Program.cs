using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PNA.BlazorWasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://api-gateway:5000") });

await builder.Build().RunAsync();
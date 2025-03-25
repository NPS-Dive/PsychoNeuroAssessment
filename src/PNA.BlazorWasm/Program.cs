using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using PNA.BlazorWasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5000")
}.AddJwtTokenHandler(builder.Configuration["Jwt:Token"]));

await builder.Build().RunAsync();

public static class HttpClientExtensions
{
    public static HttpClient AddJwtTokenHandler ( this HttpClient client, string token )
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
using AsyncStreams.BlazorWasm.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var apiBaseUrl = builder.Configuration.GetSection("ApiBaseUrl").Value;
    return new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
});

await builder.Build().RunAsync();

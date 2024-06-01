using Blazored.LocalStorage;
using Blog.Client;
using Blog.Client.AuthActions;
using Blog.Client.ClientServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<UserIntegration>();
builder.Services.AddScoped<BlogIntegration>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7105/") });

/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7105/") });*/
builder.Services.AddHttpClient("ServerApi", client => client.BaseAddress = new Uri("https://localhost:7105/"))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerApi"));
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();

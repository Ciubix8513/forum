using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using forum;
using Microsoft.AspNetCore.Components.Authorization;
using Bwasm.Cookies.Provider;
using Bwasm.Cookies.Handler;
using Blazored.LocalStorage;
using Blazored.Modal;
using Bwasm.Cookies.Logic;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthProvider>();
builder.Services.AddScoped<CookieHandler>();

builder.Services.AddHttpClient("API", options =>{
    options.BaseAddress = new Uri("https://localhost:5169");
}).AddHttpMessageHandler<CookieHandler>();
builder.Services.AddScoped<IApiLogic,ApiLogic>();
builder.Services.AddBlazoredModal();

await builder.Build().RunAsync();

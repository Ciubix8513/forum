//The GPLv3 License (GPLv3)
//
//Copyright (c) 2023 Ciubix8513
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Bwasm.Cookies.Provider;
using Bwasm.Cookies.Handler;
using Blazored.LocalStorage;
using Blazored.Modal;
using Bwasm.Cookies.Logic;
using forum;
using BlazorStrap;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazorStrap();
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

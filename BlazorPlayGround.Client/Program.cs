using BlazorPlayGround.Client;
using BlazorPlayGround.Client.Services.ClientCharacterService;
using BlazorPlayGround.Client.Services.ClientDifficultyService;
using BlazorPlayGround.Client.Services.ClientTeamService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IClientCharacterService, ClientCharacterService>();
builder.Services.AddScoped<IClientTeamService, ClientTeamService>();
builder.Services.AddScoped<IClientDifficultyService, ClientDifficultyService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();

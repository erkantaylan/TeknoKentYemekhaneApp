using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI;
using WebUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddHttpClient();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<MealRecordService>();



await builder.Build().RunAsync();
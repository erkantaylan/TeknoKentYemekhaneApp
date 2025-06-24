using Microsoft.AspNetCore.DataProtection;
using YemekhaneApp.Frontend.Components;
using YemekhaneApp.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/root/.aspnet/DataProtection-Keys"));


//var apiHttpUrl = Environment.GetEnvironmentVariable("SERVICES__API__HTTP__0");
//var apiHttpsUrl = Environment.GetEnvironmentVariable("SERVICES__API__HTTPS__0");


//var apiHttpUrl = builder.Configuration["SERVICES__API__HTTP__0"];
//var apiHttpsUrl = builder.Configuration["SERVICES__API__HTTPS__0"];

//var apiBaseUrl = apiHttpsUrl ?? apiHttpUrl;

//if (string.IsNullOrEmpty(apiBaseUrl))
//{
//    throw new Exception("API base URL bulunamadı!");
//}

builder.Services.AddHttpClient<EmployeeService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
});

builder.Services.AddHttpClient<MealRecordService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

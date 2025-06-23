using Microsoft.AspNetCore.Components.Web;
using WebUI;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var apiHttpUrl = Environment.GetEnvironmentVariable("services__api__http__0");
var apiHttpsUrl = Environment.GetEnvironmentVariable("services__api__https__0");

// Tercih etti�in hangisi ise, genelde https kullan�l�r
var apiBaseUrl = apiHttpsUrl ?? apiHttpUrl;

if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new Exception("API base URL bulunamad�!");
}

// EmployeeService HttpClient'� ekle
builder.Services.AddHttpClient<EmployeeService>(client =>
{
    client.BaseAddress = new Uri($"{apiBaseUrl}");
});

// MealRecordService HttpClient'� ekle
builder.Services.AddHttpClient<MealRecordService>(client =>
{
    client.BaseAddress = new Uri($"{apiBaseUrl}");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
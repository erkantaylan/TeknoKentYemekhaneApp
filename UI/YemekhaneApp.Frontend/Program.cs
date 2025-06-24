using YemekhaneApp.Frontend.Components;
using YemekhaneApp.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiHttpUrl = Environment.GetEnvironmentVariable("services__api__http__0");
var apiHttpsUrl = Environment.GetEnvironmentVariable("services__api__https__0");

var apiBaseUrl = apiHttpsUrl ?? apiHttpUrl;

if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new Exception("API base URL bulunamadı!");
}

builder.Services.AddHttpClient<EmployeeService>(client =>
{
    client.BaseAddress = new Uri($"{apiBaseUrl}");
});

builder.Services.AddHttpClient<MealRecordService>(client =>
{
    client.BaseAddress = new Uri($"{apiBaseUrl}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

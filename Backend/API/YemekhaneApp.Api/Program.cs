using YemekhaneApp.Persistence;
using YemekhaneApp.Application;
using Microsoft.EntityFrameworkCore;
using YemekhaneApp.Persistence.Context;
using Aspire.Hosting.ApplicationModel; // Gerekli Aspire namespace'i

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.AddSqlServerClient("db");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(builder.Configuration.GetConnectionString("DefaultConnection")); 
builder.Services.AddApplicationRegistration();

// Aspire ile gelen connection string'i kullanmak için:
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__YemekhaneDb");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// CORS eklemesi
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("EmployeeApiCors", opts =>
    {
        opts.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("EmployeeApiCors"); // CORS middleware

app.UseAuthorization();

app.MapControllers();

app.Run();

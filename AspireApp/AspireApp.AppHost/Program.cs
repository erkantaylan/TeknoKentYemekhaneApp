using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var db = builder.AddSqlServer("db")
    .WithEnvironment("ACCEPT_EULA", "Y")
    .AddDatabase("YemekhaneDb");


// API container'ý
var api = builder.AddContainer("api", "teknokentyemekhaneapp-api:latest")
    .WithReference(db)
    .WithEndpoint(8080,targetPort:8080)
    .WaitFor(db);

// UI container'ý
var ui = builder.AddContainer("webui", "teknokentyemekhaneapp-frontend:latest")
    .WithEndpoint(8080, targetPort: 8080)
    .WaitFor(api); //•	API ve UI gibi container’lar arasýnda Sadece baðýmlýlýk için .WaitFor(api) kullanmak yeterlidir.


builder.Build().Run();
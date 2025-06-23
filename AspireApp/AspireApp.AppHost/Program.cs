using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var db = builder.AddSqlServer("db")
    .WithEnvironment("ACCEPT_EULA", "Y")
    .AddDatabase("YemekhaneDb");


var api = builder.AddProject<Projects.YemekhaneApp_Api>("api")
    .WithReference(db)
    .WaitFor(db);

var ui = builder.AddProject<Projects.YemekhaneApp_Frontend>("webui")
    .WithReference(api)
    .WithExternalHttpEndpoints()
    .WithUrlForEndpoint("https", url => url.DisplayText = "Yemekhane UI")
    .WithUrlForEndpoint("http", url => url.DisplayText = "Yemekhane UI")
    .WithHttpHealthCheck("/health")
    .WaitFor(api);


builder.Build().Run();
using leca_api;
using leca_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LecaDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    }
});

builder.Services
    .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
    .AddInMemoryTokenCaches();

builder.Services.AddScoped<IGraphService, GraphService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapGet("/health", () => 200).WithName("health").WithOpenApi();
app.MapGet("/graph", async () =>
{
    string tenantId = builder.Configuration["AzureAd:tenantId"]!;
    string clientId = builder.Configuration["MicrosoftGraph:clientId"]!;
    string userPrincipalName = builder.Configuration["AzureAd:UserPrincipalName"]!;
    string clientSecret = builder.Configuration["MicrosoftGraph:clientSecret"]!;
    
    var graphHandler = new GraphHandler(tenantId, clientId, clientSecret);
    var user = await graphHandler.GetUser(userPrincipalName);
    Console.WriteLine(user?.DisplayName);
});

app.Run();

//TODO: figure out schema for initial data pull within the graph API
//build the underlying schema, start fetching data for eleazarhernandezmusic email user
//eventually, an LLM app with a team of agents are going to be able to pull from the DB and start analyzing
//store this in a different table
//present these in a small UI
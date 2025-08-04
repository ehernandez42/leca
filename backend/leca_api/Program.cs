using leca_api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LecaDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    // if (builder.Environment.IsDevelopment())
    // {
    //     options.EnableDetailedErrors();
    //     options.EnableSensitiveDataLogging();
    // }
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => 200).WithName("health").WithOpenApi();

app.Run();

//TODO: figure out schema for initial data pull within the graph API
//build the underlying schema, start fetching data for eleazarhernandezmusic email user
//eventually, an LLM app with a team of agents are going to be able to pull from the DB and start analyzing
//store this in a different table
//present these in a small UI
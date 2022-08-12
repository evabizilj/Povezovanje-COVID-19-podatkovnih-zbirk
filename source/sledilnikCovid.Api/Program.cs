using Microsoft.AspNetCore.Authentication;
using sledilnikCovid.Api.Controllers;
using sledilnikCovid.Application;
using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Infrastructure.Implementation;
using sledilnikCovid.Infrastructure.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("BasicAuth").AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", null);
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddSingleton<IFormatFetcher, SledilnikDataClient>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "sledilnikCovid.API",
        Version = "v1",
        Description = "API za sledenje Covid podatkovnim zbirkam"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

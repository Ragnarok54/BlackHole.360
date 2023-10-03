using BlackHole._360.Api.Configuration;
using BlackHole._360.Api.Filters;
using BlackHole._360.BusinessLogic;
using BlackHole._360.Common;
using BlackHole._360.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabParameterTransformer()));

                    options.Filters.Add<HttpExceptionFilter>();
                    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(NullReferenceException), StatusCodes.Status404NotFound));
                    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(string), StatusCodes.Status500InternalServerError));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});


builder.Services.AddConfiguration(builder.Configuration)
                .AddDataAccess(builder.Configuration)
                .AddBusinessServices();

builder.Services.AddHealthChecks()
                .AddDataAccessHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BlackHole 360 API",
        Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.Services.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c.AllowAnyHeader()
                  .AllowAnyOrigin()
                  .AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseHealthChecks("/api/health");

app.MapControllers();

app.Run();

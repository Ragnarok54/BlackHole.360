using BlackHole._360.BusinessLogic;
using BlackHole._360.Common;
using BlackHole._360.DataAccess;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});


builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusinessServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

app.MapControllers();

app.Run();
